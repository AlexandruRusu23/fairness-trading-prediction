import numpy
import pandas
import keras
import scipy
import matplotlib
import statsmodels
import sklearn
import theano

import data_exporter
import data_interpreter
import data_mapping

from datetime import datetime

from keras.models import Sequential
from keras.layers import Dense
from keras.wrappers.scikit_learn import KerasClassifier
from sklearn.model_selection import cross_val_score
from sklearn.preprocessing import LabelEncoder
from sklearn.model_selection import StratifiedKFold
from sklearn.preprocessing import StandardScaler
from sklearn.pipeline import Pipeline

from sklearn.model_selection import train_test_split

class Byudhra(object):
    """
    This class will be the machine learning classifier
    """

    def __init__(self):
        self.__data_frame = None
        self.__x_data = None
        self.__y_data = None
        self.__model = None

    def initialize(self):
        self.__load_data()
        self.__prepare_data()
        self.__create_and_train()

    def __load_data(self):
        data_csv_filename = "transactions_data.csv"
        data_fetcher = data_exporter.DataExporter()
        data_fetcher.fetch_data(data_csv_filename, data_interpreter.DataInterpreter.TRANSACTIONS_TABLE)
        self.__data_frame = pandas.read_csv(data_csv_filename, sep=',')
        self.__data_frame = pandas.DataFrame(self.__data_frame)
        self.__data_frame.SellerAsset = [data_mapping.asset_type_mapping[str(item).lstrip()] for item in self.__data_frame.SellerAsset]
        self.__data_frame.BuyerAsset = [data_mapping.asset_type_mapping[str(item).lstrip()] for item in self.__data_frame.BuyerAsset]
        self.__data_frame.TransactionStatus = [data_mapping.transaction_status_mapping[str(item).lstrip()] for item in self.__data_frame.TransactionStatus]

        start_dates = [datetime.strptime(item, "%Y-%m-%d") for item in self.__data_frame.OrderStartDate]
        end_dates = [datetime.strptime(item, "%Y-%m-%d") for item in self.__data_frame.OrderEndDate]
        days_diff = []
        for iter in range(len(start_dates)):
            days_diff.append(abs((start_dates[iter]-end_dates[iter]).days))
        self.__data_frame.OrderStartDate = days_diff
        self.__data_frame = self.__data_frame.drop(columns = ['OrderEndDate'])
        self.__data_frame = self.__data_frame.drop(columns = ['TransactionID'])
        self.__data_frame = self.__data_frame.drop(columns = ['SellerID'])
        self.__data_frame = self.__data_frame.drop(columns = ['BuyerID'])
        self.__data_frame.Output = [int(item) for item in self.__data_frame.Output]
        print(self.__data_frame)
    
    def __prepare_data(self):
        self.__x_data = self.__data_frame.ix[:, :6]
        self.__y_data = self.__data_frame.ix[:, 6]
        #print(self.__x_data)
        #print(self.__y_data)

    def __create_and_train(self):
        self.__model = Sequential()
        self.__model.add(Dense(12, input_dim=6, activation='relu'))
        self.__model.add(Dense(6, activation='relu'))
        self.__model.add(Dense(1, activation='sigmoid'))
        self.__model.compile(loss='binary_crossentropy', optimizer='adam', metrics=['accuracy'])
        self.__model.fit(self.__x_data, self.__y_data, epochs=150, batch_size=500)
        predictions = self.__model.predict(self.__x_data)
        #print(predictions)
        rounded = [round(x[0]) for x in predictions]
        print(rounded)
        #print(len(rounded))
    
    def test_new_data(self):
        data_csv_filename = "new_transactions_data.csv"
        data_fetcher = data_exporter.DataExporter()
        data_fetcher.fetch_data(data_csv_filename, data_interpreter.DataInterpreter.NEW_TRANSACTIONS_TABLE)

        new_data_frame = pandas.read_csv(data_csv_filename, sep=',')
        new_data_frame = pandas.DataFrame(new_data_frame)
        new_data_frame.SellerAsset = [data_mapping.asset_type_mapping[str(item).lstrip()] for item in new_data_frame.SellerAsset]
        new_data_frame.BuyerAsset = [data_mapping.asset_type_mapping[str(item).lstrip()] for item in new_data_frame.BuyerAsset]
        new_data_frame.TransactionStatus = [data_mapping.transaction_status_mapping[str(item).lstrip()] for item in new_data_frame.TransactionStatus]

        start_dates = [datetime.strptime(item, "%Y-%m-%d") for item in new_data_frame.OrderStartDate]
        end_dates = [datetime.strptime(item, "%Y-%m-%d") for item in new_data_frame.OrderEndDate]
        days_diff = []
        for iter in range(len(start_dates)):
            days_diff.append(abs((start_dates[iter]-end_dates[iter]).days))
        new_data_frame.OrderStartDate = days_diff
        new_data_frame = new_data_frame.drop(columns = ['OrderEndDate'])
        transactions_id = new_data_frame.TransactionID
        new_data_frame = new_data_frame.drop(columns = ['TransactionID'])
        new_data_frame = new_data_frame.drop(columns = ['SellerID'])
        new_data_frame = new_data_frame.drop(columns = ['BuyerID'])
        new_data_frame.Output = [int(item) for item in new_data_frame.Output]
        #print(new_data_frame)
        new_x_data = new_data_frame.ix[:, :6]
        #new_y_data = new_data_frame.ix[:, 6]

        predictions = self.__model.predict(new_x_data)
        print('Exact prediction value: ' + str(predictions))
        rounded = [round(x[0]) for x in predictions]
        print('Rounded prediction value: ' + str(rounded))
        print('No. of predictions: ' + str(len(rounded)))

        # update the table
        data_fetcher.insert_tested_data(transactions_id, rounded, data_interpreter.DataInterpreter.NEW_TRANSACTIONS_TABLE)
