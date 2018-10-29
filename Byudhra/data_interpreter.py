"""
Convert data from db into byudhra acceptable representation
"""

class DataInterpreter(object):
    """
    Data Interpreter class
    """
    # transactions table columns indexed
    TransactionID = 0
    SellerID = 1
    SellerAsset = 2
    SellerAmount = 3
    BuyerID = 4
    BuyerAsset = 5
    BuyerAmount = 6
    TransactionStatus = 7
    OrderStartDate = 8
    OrderEndDate = 9

    #company columns table columns indexed
    CompanyID = 0
    CompanyName = 1
    CompanyLocation = 2
    CompanyLatitude = 3
    CompanyLongitude = 4
    CompanyPopularity = 5

    #table names
    COMPANY_TABLE = 'Company'
    TRANSACTIONS_TABLE = 'TransactionTable'
    NEW_TRANSACTIONS_TABLE = 'TransactionTableCopy'
