import csv
import sys

dados = sys.argv[1]

data_large = open(f"data_large/data/{dados}.csv","r")
data_tiny = open(f"data_tiny/dataset/data/{dados}.csv","r")

dados_large = csv.reader(data_large,delimiter=';')
dados_tiny = csv.reader(data_tiny,delimiter=";")

map = {}
lista_novos = []
lista_alterados = []
header = ""

for item in dados_large:
    
    if header == "":
        header = ";".join(item)
    else:
        map[item[0]] = "".join(item)

header_tiny = False

for item in dados_tiny:

    if header_tiny is False:
        header_tiny = True
        continue

    id = item[0]

    if id in map:

        if map[id] != "".join(item):
            lista_alterados.append(";".join(item))

    else:
        lista_novos.append(";".join(item))

output_novos = open(f"{dados}_new.csv","w+")
output_novos.write(f"{header}\n")
print("New entries:")

for item in lista_novos:

    print(f"NEW : {item}")
    output_novos.write(f"{item}\n")

output_alterados = open(f"{dados}_changed.csv","w+")
output_alterados.write(f"{header}\n")
print("Changed entries:")

for item in lista_alterados:

    print(f"CHANGED : {item}")
    output_alterados.write(f"{item}\n")

