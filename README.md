# AtmoTrack : Data Logger Ambiental com Arduino

# PBL_datalloger

Trabalho de desenvolvimento de um datalogger para disciplina de Sistemas Embarcados de Engenharia da Computação da FESA - Faculdade de Engenheiro Salvador Arena

# Manual de Instruções 

Este manual descreve todos os componentes , materias e funcionalidades do dispositivo.

## Datasheets dos Componentes

Aqui estão os datasheets dos componentes usados no projeto:

- [ATMEGA48P](Datasheets/Datasheet-ATMEGA48P.PDF)
- [Batereia 9V](Datasheets/Datasheet-Bateria9V.pdf)
- [Buzzer](Datasheets/Datasheet-Buzzer.pdf)
- [DHT11](Datasheets/Datasheet-DHT11.PDF)
- [LDR](Datasheets/Datasheet-LDR.pdf)
- [Protoboard](Datasheets/Datasheet-Protoboard.pdf)
- [RTCDS1307](Datasheets/Datasheet-RTCDS1307.PDF)
- [SSD1306](Datasheets/Datasheet-SSD1306.PDF)

## Objetivo

Esse projeto foi criado para fazer o monitoramento ambiental de temperatura, umidade e luminosidade, usando sensores conectados a um Arduino Uno (ATmega328P). Ele exibe os dados em um display OLED e armazena tudo na memória EEPROM com uma marcação de tempo. Além disso, tem alertas sonoros e visuais quando os valores saem dos limites que você definir.

## Materiais Necessários

- **Microcontrolador**: Arduino Uno (ATmega328P) (Uma unidade)
- **Sensores**:
  - DHT11/DHT22: Medição de temperatura e umidade (Uma unidade)
  - LDR: Medição de luminosidade (Uma unidade)
- **Display**: OLED 128x64 pixels I2C (Uma unidade)
- **Memória**: EEPROM para guardar os dados (Uma unidade)
- **RTC**: Módulo DS3231 (Relógio de Tempo Real) para registrar a hora dos eventos (Uma Unidade)
- **Buzzer**: Para os alertas sonoros (Uma unidade)
- **LEDs**: Para alertas visuais e status (Uma unidade) 
- **Botões**: Três botões (UP, DOWN e SELECT) para navegação e configuração(Três unidades)
- **Outros**: Jumpers, resistores, bateria 9v e uma protoboard(Uma unidade de cada)

## Esquema de Montagem

### Conexões:

- **DHT22**: Conectado ao pino digital 2 do Arduino.
- **LDR**: Vai no pino analógico A0, com um resistor de pull-down.
- **Display OLED**: Conectado ao barramento I2C (pinos GND , alimentação 3.3V , pino A4 para SDA e A5 para SCL).
- **RTC DS3231**: Também usa o barramento I2C.
- **EEPROM**: Conectada via I2C.
- **Botões de controle**: Conectados nos pinos digitais 6, 7 e 8.
- **Buzzer**: Vai no pino digital 5.

### Diagrama de Montagem:

Siga o diagrama da protoboard para fazer as conexões corretas. Vai ajudar a organizar e garantir que tudo funcione bem.

## Configuração do Sistema

### 1. Compilação e Upload do Código

1. Abra o Arduino IDE e conecte seu Arduino ao computador usando um cabo USB.
2. Copie o código do arquivo `sketch.ino` para o Arduino IDE.
3. Certifique-se de que as bibliotecas necessárias estão instaladas (você pode usar o Library Manager para isso):
   - `SPI.h`, `Wire.h`, `Adafruit_GFX.h`, `Adafruit_SSD1306.h`, `RTClib.h`, `EEPROM.h`, `DHT.h`
4. Compile o código e faça o upload para o Arduino.

### 2. Como o Sistema Funciona

#### Inicialização:

- Ao ligar, o sistema exibe o logotipo e o nome (AtmoTrack) no display OLED.
- O RTC é configurado para registrar data e hora dos eventos.

#### Monitoramento:

- **Temperatura**: Captada pelo DHT22 e exibida no display.
- **Umidade**: Captada pelo DHT22 e exibida no display.
- **Luminosidade**: Lida pelo LDR e exibida no display.

Se a temperatura ou a umidade sair dos limites pré-definidos, ativa efeitos sonoros (buzzer), além de registrar os dados na EEPROM com um timestamp.

#### Navegação:

- Use os botões UP, DOWN e SELECT para navegar pelo  menu,  ajustar os limites de alerta.

### 3. Parâmetros Configuráveis

Você pode configurar os seguintes limites:

- **Temperatura mínima**: 20.0°C
- **Temperatura máxima**: 30.0°C
- **Umidade mínima**: 30%
- **Umidade máxima**: 60%

## Operação do Sistema

### 1. Exibição dos Dados

O display OLED vai alternar entre as informações de temperatura, umidade e luminosidade, além da data e hora fornecidas pelo módulo RTC no ângulo superior direito .

### 2. Alertas

Quando algum valor sair dos limites:

- **LEDs**: Piscam para chamar atenção.
- **Buzzer**: Emite um som de alerta.

### 3. Armazenamento de Dados

Os dados (temperatura, umidade e luminosidade) são salvos na EEPROM a cada minuto, mas somente se dair dos parâmetros.

### 4. Memória EEPROM

O sistema pode armazenar até 100 registros na EEPROM, incluindo temperatura, umidade, luminosidade e o horário de cada registro.

### 5. Monitoramento da Memória RAM

O sistema também fica de olho na quantidade de RAM disponível. Se estiver muito baixa, ele vai mostrar uma mensagem no monitor serial para evitar possíveis falhas.

### 6. Histórico de erros

O sistema possuir um menu aonde o usuário pode conferir o histórico de erros dentro da propria tela para o usuário , aonde pelos botões ele pode interagir com o menu e conferir.

### link da simulação
```
https://wokwi.com/projects/408601927738101761
```
