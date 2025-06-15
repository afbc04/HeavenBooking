# Projeto HeavenBooking

**Nome do Projeto :** HeavenBooking  
**Data de Inicio :** 14/06/2025  
**Data de Conclusão:** ---  
**Autor :** André Campos

![Foto do Autor](Report/author.png)


## Índice

- [1. Introdução](#1-introdução)
- [2. Recolha do Dataset](#2-recolha-do-dataset)
- [3. Requisitos](#3-requisitos)
- [4. Preparação e Tratamento dos Dados](#4-preparação-e-tratamento-dos-dados)
- [5. Arquitetura](#5-arquitetura)
- [6. Esboço de Interfaces](#6-esboço-de-interfaces)
- [7. Persistência de Dados](#7-persistência-de-dados)
- [8. Lógica de Negócio](#8-lógica-de-negócio)
- [9. Interface de Utilizador](#9-interface-de-utilizador)
- [10. Outras Features](#10-outras-features)
- [11. Testes e Gestão do Sistema](#11-testes-e-gestão-do-sistema)
- [12. Conclusão](#12-conclusão)

## 1. Introdução

### 1.1 Motivação e Objetivos

Este projeto é um trabalho extra-curricular com os **objetivos** de:
- Criar uma aplicação Web
- Criar um projeto que siga a arquitetura Web de 3 camadas
- Guardar os ficheiros em disco, de forma persistente, usando uma base de dados relacional com objetivo de aprimorar o conhecimento de Base de Dados
- Criar uma Lógica de Negócio usando uma nova linguagem de programação orientada a objetos
- Explorar formas eficientes de manipular e usar milhares de dados
- Desenvolver uma Interface de Utilizador utilizando HTML, CSS e JavaScript com o objetivo de aprimorar o conhecimento em UI
- Criar dois serviços principais : Frontend e Backend
- Utilizar sockets e HTTP  
- **Criar um sistema complexo e completo!**

### 1.2. Tema

Como o projeto é um Sistema de Gestão de bastantes dados, decidi reutilizar e aprimorar um projeto da Unidade Curricular **Laboratórios Informáticos III**  
_Enunciado entra-se [aqui](Report/Enunciado-LI3.pdf)_

Neste projeto, temos as seguintes entidades:
1. **Utilizadores :** os utilizadores são registados no sistema, podendo estar ativos ou não, que contêm reservas ou voos do utilizador
2. **Reservas :** as reservas pertencem a um único utilizador e pertencem a um único Hotel. As reservas contêm informações sobre o preço das mesmas
3. **Hoteis :** os hoteis são edificios que contêm informações sobre as suas localizações, características e as reservas efetuadas neles
4. **Voos :** cada voo contém uma lista de utilizadores, que são os passageiros, as características do avião que efetua o voo e os aeroportos de partida e destino do voo
5. **Aeroportos :** os aeroportos são localidades onde os voos partem ou chegam

Nesse projeto, foram pedidos que houvessem três funcionalidades principais:
1. **Batch :** os dados e um conjunto de queries eram lidas e o programa apenas tinha que resolver as queries indicadas
2. **Interativo :** os dados eram lidos e o utilizador poderia ativamente executar queries, decidindo quando poderia parar o sistema
3. **Modo de testes :** o resultados das queries eram comparados com um conjunto de ficheiros corretos para indicar se o sistema executa as queries corretamente

Como este projeto tem o objetivo de ser uma aplicação web, ela terá:
- **Modo de testes** para verificar se o sistema está correto
- Faz muito mais sentido fazer uma abordagem do modo **interativo**, então o modo batch será ignorado

## 2. Recolha do Dataset

A escolha de datasets é o principal foco neste projeto, pois todo o projeto anda à volta deles.
Como dito acima, decidi utilizar os datasets atribuídos no projeto currícular (Laboratórios Informáticos III), pois:
- Tem vários dados diversos
- Existem muitos metadados

Os datasets em questão têm este formato:

1. **Dataset de Utilizadores**  
![Imagem com o dataset de Utilizadores](Report/datasets/old_users.png)

2. **Dataset de Reservas**  
![Imagem com o dataset de Reservas](Report/datasets/old_reservations.png)

3. **Dataset de Voos**  
![Imagem com o dataset de Voos](Report/datasets/old_flights.png)

4. **Dataset de Passageiros**  
![Imagem com o dataset de Passageiros](Report/datasets/old_passengers.png)

### 2.1. Alteração dos Datasets

Decidi reorganizar os datasets e acrescentar mais informação para que o sistema tivesse mais informação para apresentar

1. **Utilizadores**
    - Juntei os utilizadores dos dois datasets
    - Como já haviam bastante atributos, não adicionei nenhum
    - Alterei o país, emails, moradas e número de telemóvel dos utilizadores de forma a não haver 100% utilizadores portugueses

![Imagem com o dataset novo de Utilizadores](Report/datasets/new_users.png)

2. **Reservas**
    - Juntei as reservas dos dois datasets
    - Atribuí um novo campo chamado "Refund", que indica se uma reserva foi reembolsada
    - Retirei os atributos dos hoteis para os colocar num outro dataset dedicado a hoteis

![Imagem com o dataset novo de Reservas](Report/datasets/new_reservations.png)

3. **Hoteis**
    - Dataset novo criado com base nos dados encontrados nas reservas
    - Para além dos campos extraídos, foram adicionados novos campos, tais como:
        - País
        - Código do País
        - Link do website do hotel
        - Número de telemóvel
        - Código Pin
        - Se está ativo
    - Foram adicionados novos hoteis, que são marcados como inativos pois não serão utilizados para realizar reservas
    - Os dados foram pesquisados na internet através do site [booking.com](https://booking.com), através de um dataset disponibilizado no [kaggle.com](https://www.kaggle.com/datasets/raj713335/tbo-hotels-dataset) e através de informações ao pesquisar pelo nome dos hoteis

![Imagem com o dataset de Hoteis](Report/datasets/new_hotels.png)

4. **Voos**
    - Juntei os voos dos dois datasets
    - Atribuí um novo campo chamado "Cancelled", que indica se um voo foi cancelado
    - Já havia bastante metadata e o dataset apenas dá a identificação de aeroportos, não as suas características

![Imagem com o dataset novo de Voos](Report/datasets/new_flights.png)

5. **Aeroportos**
    - Dataset novo criado com base nos dados encontrados nos voos
    - A única informação dada era a identificação do aeroporto, então decidi adicionar a seguinte metainformação:
        - Nome
        - Código IATA, se disponível
        - País onde se localiza
        - Código do País
        - Ano de inauguração
        - Se está ativo ou não
    - Foram adicionados novos aeroportos, que poderão estar ativos ou não
    - Os dados foram coletados através do site [openflights.org](https://www.openflights.org/data)

![Imagem com o dataset de Aeroportos](Report/datasets/new_airports.png)

6. **Passageiros**
    - Juntei os passageiros dos dois datasets
    - Atribuí um novo campo chamado "User Arrived", que indica se um utilizador embarcou num voo
    - Este dataset é uma tabela de união, então não é preciso adicionar mais metadados ou efetuar alterações

![Imagem com o dataset novo de Passageiros](Report/datasets/new_passengers.png)

### 2.2. Análise e Resultado das alterações efetuadas

As metainformações de cada dataset são:

**1. Utilizadores**

| Nome do Metadado | Significado do Metadado |
|:-----------------|-----------|
| `ID` | Identificador do Utilizador |
| `Name` | Nome |
| `Email` | Email |
| `Phone Number` | Número de Telemóvel |
| `Birth Date` | Data de Nascimento |
| `Sex` | Género |
| `Passport` | Passaporte |
| `Country Code` | Código do País de origem |
| `Address` | Morada |
| `Account Creation` | Data de criação de conta (data e hora) |
| `Pay Method` | Método de Pagamento |
| `Account Status` | Estado atual da conta |

**2. Reservas**

| Nome do Metadado | Significado do Metadado |
|:-----------------|-----------|
| `ID` | Identificador da Reserva |
| `User ID` | Identificador do Utilizador que a reservou |
| `Hotel ID` | Identificador do Hotel a quem a reserva pertence |
| `Begin Date` | Data de Inicio |
| `End Date` | Data de Fim |
| `Price Per Night` | Preço por Noite |
| `Includes Breakfast` | Indica se a reserva inclui pequeno-almoço |
| `Refunded` | Indica se a reserva foi reembolsada |
| `Room Details` | Detalhes sobre o quarto |
| `Rating` | Classificação (opcional) atribuída pelo utilizador |
| `Comment` | Comentário (opcional) sobre a reserva |

**3. Hoteis**

| Nome do Metadado | Significado do Metadado |
|:-----------------|-----------|
| `ID` | Identificador do Hotel |
| `Name` | Nome |
| `Stars` | Estrelas do hotel (1 a 5) |
| `City Tax` | Percentagem do imposto da cidade sobre o valor total (valores inteiros) |
| `Address` | Morada do hotel |
| `Country` | País onde o hotel se localiza |
| `Country Code` | Código do país |
| `Website` | URL do website do hotel |
| `Phone Number` | Número de telemóvel do hotel |
| `Pin Code` | Código PIN do hotel |
| `Active` | Indica se o hotel está ativo |

**4. Voos**

| Nome do Metadado | Significado do Metadado |
|:-----------------|-----------|
| `ID` | Identificador do Voo |
| `Airline` | Companhia aérea |
| `Plane Model` | Modelo do avião |
| `Total Seats` | Número de lugares totais disponíveis |
| `Cancelled` | Indica se o voo foi cancelado |
| `Origin` | Identificador do aeroporto de origem |
| `Destination` | Identificador do aeroporto de chegada |
| `Schedule Departure Date` | Data e hora estimada de partida |
| `Schedule Arrival Date` | Data e hora estimada de chegada |
| `Real Departure Date` | Data e hora real de partida |
| `Real Arrival Date` | Data e hora real de chegada |
| `Pilot` | Nome do piloto |
| `Copilot` | Nome do copiloto |
| `Notes` | Observações sobre o voo |

**5. Aeroportos**

| Nome do Metadado | Significado do Metadado |
|:-----------------|-----------|
| `ID` | Identificador do Aeroporto |
| `Name` | Nome |
| `IATA` | Código IATA, se disponível |
| `Country` | País onde o aeroporto localiza-se |
| `Country Code` | Código de país |
| `Start Date` | Ano de inauguração do aeroporto |
| `Active` | Indica se o aeroporto está ativo |

**6. Passageiros**

| Nome do Metadado | Significado do Metadado |
|:-----------------|-----------|
| `Flight ID` | Identificador do Voo |
| `User ID` | Identificador do Utilizador |
| `User Arrived` | Indica se o utilizador embarcou no voo |


**Considerações :**
- As datas devem seguir o formato `aaaa/mm/dd` ou `aaaa/mm/dd hh:mm:ss` em datas com tempo
- Os números decimais deverão ser arrendondadas a três casas décimais
- O custo total de uma reserva calcula-se com base na seguinte formúla : `preço_por_noite * número de noites + ( (preço_por_noite * número_de_noites) / 100 ) * imposto_da_cidade`

**Resultado dos novos Datasets :**

|  | Utilizadores | Reservas | Hoteis | Voos | Aeroportos | Passageiros |
|:-----------------|:-----------:|:-----------:|:-----------:|:-----------:|:-----------:|:-----------:|
| **Dados do dataset grande** | 990133 | 5936637 | 90 | 179945 | 27 | 14691055 |
| **Dados do dataset pequenos novos** | 5551 | 564 | 0 | 103 | 0 | 8045 |
| **Dados do dataset pequenos alterados** | 3913 | 37794 | 0 | 847 | 0 | 65282 |
| **Dados novos adicionados** | 0 | 0 | 9 | 0 | 8 | 0 |
| **Dados do dataset final** | 995684 | 5937201 | 99 | 180048 | 35 | 14699100 |

## 3. Requisitos

## 4. Preparação e Tratamento dos Dados

## 5. Arquitetura

## 6. Esboço de Interfaces

## 7. Persistência de Dados

## 8. Lógica de Negócio

## 9. Interface de Utilizador

## 10. Outras Features

## 11. Testes e Gestão do Sistema

## 12. Conclusão
