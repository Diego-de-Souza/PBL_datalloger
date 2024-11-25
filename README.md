# AtmoTrack : Estufa e datalogger para secagem de enrolamento de motores.

# PBL_datalloger

Trabalho de desenvolvimento de um datalogger para as disciplinas de Sistemas Embarcados, Linguagem de Programação I, Controle e Automação, Mecânica dos Sólidos e fenômenos de transporte de Engenharia da Computação da FESA - Faculdade de Engenheiro Salvador Arena

# Manual de Instruções 

Este manual descreve todos os componentes , materias e funcionalidades do dispositivo.

## Datasheets dos Componentes

Aqui estão os datasheets dos componentes usados no projeto:

- [ATMEGA48P](Datasheets/Datasheet-ATMEGA48P.PDF)
- [Batereia 9V](Datasheets/Datasheet-Bateria9V.pdf)
- [DHT11](Datasheets/Datasheet-DHT11.PDF)
- [Protoboard](Datasheets/Datasheet-Protoboard.pdf)
- [RTCDS1307](Datasheets/Datasheet-RTCDS1307.PDF)
- [SSD1306](Datasheets/Datasheet-SSD1306.PDF)
- 

## Objetivo

Esse projeto foi criado para fazer o monitoramento de temperatura remoto de estufas de secagem de enrolamentos de motores. O sistema consta com o microcontroledor ESP-32 para a captação e envio dos dados de temperatura para a nuvem e site ArmoTrack. 

## Materiais Necessários

- **Microcontrolador**:
  - ESP-32 (Uma unidade)
  - Arduito (Atmega 328P) (uma unidade)
- **Sensores**:
  - DHT11: Sensor para captação de temperatura
- **Outros**: Jumpers, resistores, display LCD 16x2, bateria 9v e uma protoboard(Uma unidade de cada)

## Esquema de Montagem

### Conexões:

- **DHT11**: Conectado ao Arduino.
- **Display LCD**: Conectado ao Arduino.
- **ESP-32**: Conectado ao Arduino via pino 34.

### Diagrama de Montagem:

Siga o diagrama da protoboard para fazer as conexões corretas. Vai ajudar a organizar e garantir que tudo funcione bem.

## Configuração do Sistema

### 1. Compilação e Upload do Código

1. Abra a ESP-32 e conecte seu Arduino ao computador usando um cabo USB.
2. Copie o código do arquivo `sketch.ino` para o Arduino IDE.
3. Certifique-se de que as bibliotecas necessárias estão instaladas (você pode usar o Library Manager para isso)
4. Compile o código e faça o upload para o Arduino.
5. Acesse o site Atmotrack com login e senha para cadastrar seu dispositivo ESP-32 e iniciar o monitoramento em tempo real da sua estufa

### 2. Como o Sistema Funciona

#### Inicialização:

- Ao abrir, o site exibe a página inicial AtmoTrack, sinta-se à vontade para explorar a página e conhecer mais sobre o datalogger.
- Caso deseje acessar os Dashboards, efetue seu login ou cadastro e selecione o dispositivo que deseja monitorar.

#### Monitoramento:

- **Temperatura**: Captada constantemente e armazenada a cada um minuto, o valor armazenado é uma média dos valores coletados durante o intervalo de tempo.

Se a temperatura sair dos limites pré-definidos, ativa efeitos visuais (LED interno ESP-32).

## Operação do Sistema

### 1. Exibição dos Dados

O gráfico exibirá os pontos armazenados em nuvem, o gráfico reflete em tempo real a temperatura da estufa.

### 2. Alertas

Quando algum valor sair dos limites:

- **LEDs**: Piscam para chamar atenção.

### 3. Armazenamento de Dados

Os dados são armazenados em nuvem por meio de dois bancos de daodos distintos.
-**MONGO DB**: Armazena dados referentes à temperatura.
-**SQL Server**: Armazena dados de úsuario e dispositivo, assim como empresa.

### link da simulação
```
https://wokwi.com/projects/408601927738101761
```
