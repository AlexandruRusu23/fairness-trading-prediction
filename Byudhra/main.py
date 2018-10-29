import byudhra
import time

if __name__ == '__main__':
    byudhra_model = byudhra.Byudhra()
    byudhra_model.initialize()

    while input('Do you want to test? (y/exit)') != 'exit':
        time.sleep(100.0/1000.0)
        byudhra_model.test_new_data()
