
import urllib.request

csv_url = 'https://dados.antt.gov.br/dataset/8260c151-edaa-4620-82d6-cf2080374e63/resource/837838e7-a4ff-4542-b7f5-0df3e10998c3/download/operador_transporte_multimodal.csv'

urllib.request.urlretrieve(csv_url, 'operador_transporte_multimodal.csv')