import pandas as pd
# from kedro.extras.datasets.pandas import SQLQueryDataSet
# import pyodbc 
# import urllib
from sqlalchemy import create_engine
# import logging
 
server = "gfnwsql.database.windows.net"
database = "DataNavigation"
username = "mathep20"
password = 'vamoS-321'
driver = 'SQL Server'
 
DATABASE_CONNECTION = f'mssql+pyodbc://{username}:{password}@{server}/{database}?driver={driver}'
engine = create_engine(DATABASE_CONNECTION)
connection = engine.connect()
data = pd.read_sql_query("select * from OpenWeather", connection)
print(data)