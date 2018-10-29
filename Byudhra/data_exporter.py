"""
Module responsible to fetch data from the database
Export the data as a csv file
"""
import pyodbc
import data_interpreter

class DataExporter(object):
    """
    DataExporter class
    """
    def __init__(self):
        self.__server = 'tcp:findmyorderserver.database.windows.net,1433'
        self.__database = 'FindMyOrder'
        self.__username = 'user@findmyorderserver'
        self.__password = 'Password01?'
        self.__driver = '{SQL Server}'
        self.__connection_string = \
            "Driver={driver};\
            Server={server};\
            Database={database};\
            Uid={username};\
            Pwd={password};\
            Encrypt=yes;\
            TrustServerCertificate=no;\
            Connection Timeout=30;".format(
                driver=self.__driver,
                server=self.__server,
                database=self.__database,
                username=self.__username,
                password=self.__password
                )
        self.__cnxn = None
    
    def fetch_data(self, csv_filename, table_name):
        self.__cnxn = pyodbc.connect(self.__connection_string)
        cursor = self.__cnxn.cursor()
        cursor.execute("SELECT * from {table_name}".format(table_name = table_name))
        row = cursor.fetchone()

        csv_datafile = open(csv_filename, "w+")
        table_columns_name = ''
        
        if table_name is data_interpreter.DataInterpreter.TRANSACTIONS_TABLE or table_name is data_interpreter.DataInterpreter.NEW_TRANSACTIONS_TABLE:
            table_columns_name = ['TransactionID', 'SellerID', 'SellerAsset', 'SellerAmount', 'BuyerID', 'BuyerAsset', 'BuyerAmount', 'TransactionStatus', 'OrderStartDate', 'OrderEndDate', 'Output']

        if table_name is data_interpreter.DataInterpreter.COMPANY_TABLE:
            table_columns_name = ['CompanyID', 'CompanyName', 'CompanyLocation', 'CompanyLatitude', 'CompanyLongitude', 'CompanyPopularity']

        csv_datafile.write(','.join(table_columns_name) + '\n')
        while row is not None:
            row = [str(item) for item in row]
            csv_datafile.write(','.join(row) + '\n')
            row = cursor.fetchone()
    
    def insert_tested_data(self, data_frame_ids, output, table_name):
        cursor = self.__cnxn.cursor()
        for iter in range(len(output)):
            cursor.execute("UPDATE {table_name} SET Output = {value} WHERE TransactionID = {id}".format(table_name = table_name, value = output[iter], id = data_frame_ids[iter]))
            print("UPDATE {table_name} SET Output = {value} WHERE TransactionID = {id}".format(table_name = table_name, value = output[iter], id = data_frame_ids[iter]))
            self.__cnxn.commit()
