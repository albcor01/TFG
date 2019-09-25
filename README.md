# Herramienta generadora de música adaptativa para videojuegos

### Propuesto por:
- Juan Ruiz Jiménez (juarui02@ucm.es)
- Ramón Arjona Quiñones (ramonarj@ucm.es)
- Alberto Córdoba Ortiz (albcor01@ucm.es)
## Objetivo
El objetivo principal del TFG es crear un plugin para el motor de videojuegos Unity3D. Estará dotado de una interfaz gráfica integrada en el motor para permitir al usuario definir tanto el estilo de la música a generar (crear un momento triste, de terror, de acción, etc) como su forma de adaptarse al juego. 

A continuación se lista cada uno de los sub-objetivos en los que descomponer el objetivo principal:

* Crear la música de forma procedural.
* Generarla en función a distintos estilos.
* Modificar y adaptar la música en ejecución.
* Crear una interfaz gráfica para el usuario.
* Refinar tanto los algoritmos de generación como la adaptación según los parámetros dados.


## Funcionamiento
La herramienta constará de 2 partes diferenciadas:

### 1. Generación procedural de música:
La música se genera siguiendo distintos algoritmos que aprovechan la relación entre los grados musicales. El usuario puede elegir el estilo de la canción, además de otros parámetros como el tempo. Todo esto afectará al funcionamiento de dicho algoritmo. E.g.: el usuario escoge una música de aventura a 140bpm.

### 2. Adaptación  de la música generada
La música se adapta al estado actual del juego y a los eventos que ocurren. El usuario debe indicar ternas de {input,output,efecto} para definir el funcionamiento de dicha adaptación. 
El input se corresponde con variables del juego que el jugador elige de su código.
El output se elige entre un banco de parámetros proporcionados por la aplicación.
El efecto indica de qué forma se relacionan el input y el output.
E.g.: la velocidad del jugador (input) afecta al tempo de la música (output) de forma directamente proporcional (efecto).

## Metodología
Se dividirá el trabajo entre los integrantes de acuerdo a los sub-objetivos antes mencionados con el objetivo de agilizar el desarrollo, habiendo un responsable para cada aspecto del TFG:
Generación 
Adaptación  
Integración en el motor

Aunque sin decidir todavía, se prevé que se usará la plataforma SuperCollider para la realización del TFG, ya que da soporte para la composición algorítmica y usa el protocolo OSC para comunicarse en tiempo real con la red.
