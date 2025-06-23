import csv

csv_path = input("Put csv path > ")

unique_ids = set()

with open(csv_path, newline='', encoding='utf-8') as csvfile:
    reader = csv.DictReader(csvfile, delimiter=';')
    for row in reader:
        unique_ids.add(row['id'])

print(f'Total de IDs únicos: {len(unique_ids)}')
