### 25/09/19
- Primera reunión con Jaime y Marco, los tutores del proyecto. Se define el primer objetivo: 
enlazar Unity con SuperCollider.

### 27/09/19
- Primer test del proyecto, se consigue enlazar con éxito Unity con SuperCollider. Se puede enviar
desde Unity la orden de lanzar música a SuperCollider. Código base en "SCScripts".

### 02/10/19
- Primera prueba para obtener la ventana del futuro Plugin.
Para crear una ventana propia de Unity solo hay que hacer una clase que herede de "EditorWindow",
y sobreescribir los métodos "init", "onGUI" y "update". El ejemplo está en el archivo "OSCHelper.cs".
Referencia de Unity en: https://docs.unity3d.com/Manual/editor-EditorWindows.html

### 08/10/19
- Con los primeros sintetizadores y la posibilidad de enviar mensajes a SuperCollider, se establece
una primera demo a modo de prueba para comprobar el funcionamiento base. Se lanza música desde el juego
con la posibilidad de añadir percusión dinámicamente y cambiar el tempo de la pieza. 
Código en "SCtestCode.scd".

### 09/10/19 
- Segunda reunión con los tutores. Se establece como siguiente objetivo tener una ventana funcional, 
con la posibilidad de recibir información desde SuperCollider y una progresión músical básica, pero 
con sentido. Se decide enfatizar, de momento, en la riquiza tímbrica más que en la complejidad
armónica, tomando como ejemplo a artistas como Brian Eno o C418.

### 11/10/19
- Ampliada funcionalidad de la ventana del Plugin. Se puede acceder a las variables de los objetos
a tener en cuenta a la hora de adaptar la música desde el Editor.

### 12/10/19
- Se consigue enviar información desde SuperCollider a Unity. Primera prueba haciendo un Log del tempo
de la pieza en el Editor.
Código en "SCScripts".

### 21/10/19
- Se define la estructura base de SuperCollider con el sistema de eventos. Los primeros eventos se basan
en osciladores y cómo modificar su cuerpo a través de generadores LF. 

### 02/02/20
- Se cambia el modelo con el que funciona el plugin en Unity; en vez de una ventana del editor, se presenta como un 
script que deberá llevar un GameObject vacío y donde se podrán configurar las tuplas {input-efecto-output} para 
la música adaptativa. De esta forma la organización interna es mucho más sencilla y la persistencia se 
aprovecha de la escena de Unity sin necesidad de guardar datos a ningún fichero.
- Se eliminan por tanto algunos Scripts innecesarios (PluginTFG y MMManager)

### 04/02/20
- Se consigue que solo se manden los mensajes a SuperCollider en el caso de que las variables de entrada
hayan modificado su valor. Empezada distinción de casos para procesar los distintos tipos de mensaje.
- Se crea "MMTest.sc", un archivo de SuperCollider parecido al "Events(Test).sc" para hacer pruebas 
de comunicación desde Unity. NOTA: el script OSCCall ya no es necesario, lo único que hacía se hace ahora en 
el "Start()" de MusicMaker.cs
- Interfaz básica con un par de botones en MMTest.unity

### 05/02/20
Se ha conseguido que los mensajes que llegan de supercollider a unity sean coherentes. Antes los mensaje llegaban 
de manera que se añadian a una lista y nunca se iban además se volcaba el ultimo paquete recibido en cada tick porque
nunca volvia a ser null ESTO HABRIA QUE MIRAR SI SE ACTUALIZA EN CADA TICK O SOLO CUANDO LLEGA NUEVA INFORMACION PORQUE 
ENTONCES CAMBIA. habiendo realizado cambios en el codigo de OSCBundle he conseguido definitivamente que los datos sean coherentes.

### 10/02/20
- Reunión interna para acalarar decisiones de diseño respecto a las 2 partes diferenciadas del proyecto:

#### 1. Música procedural 
Se decide que el principal sesgo para la generación será establecer la temática de entre 
un abanico (de tamaño por definir) de ellos. Cada temática se considerará como un "pack", ya que traen unos parámetros predeterminados
(tempo, tipos de acordes, número de capas, efectos, instrumentos, etc). Aparte de la temática, el usuario podrá 
decidir algunos parámetros más, como la duración de la pieza o la tonalidad de esta.

#### 2. Música adaptativa
Se decide que los inputs que introduzca el jugador (las variables) podrán ser únicamente de tipo
bool, int o float. No creemos necesario usar ningún tipo más (los strings no tienen sentido en este contexto, los chars
quedan por decidirse). Los tipos no booleanos, además, cuentan con un mínimo y un máximo para dar un contexto a la variable que recibimos.
- Los outputs de la música no están especificados todavía, pero entre ellos se encuantra el tempo, nºcapas, 
- No se incluyen entre los outputs efectos de sonido (FX), pero puede que alguna capa, aún tratándose de música, pueda suplir esta función
si el usuario es capaz de usarlo convenientemente (e.g.: un booleano que activa una pista nueva en el momento en el que el jugador ataca)

### 11/02/20
- Se decide que los valores de tipo int y float se mandarán a SuperCollider normalizados entre {0-1},
siendo 0.5 el valor por defecto (i.e., el parámetro se pondrá en su valor por defecto para el tópico actual), 
y 0 y 1 el mínimo y el máximo establecidos en SuperCollider para ese parámetro.
[ Ej: Mandamos un mensaje ("tempo", 0.9) desde Unity. En SuperCollider, si el rango de tempo para el tópico actual está entre 100 y 200
(siendo el 150 por defecto), se establecerá el tempo como 190 ]

### 12/02/20
- Añadida la normalización entre 0 y 1 de los parámetros de tipo int y float, así como los casos de "Deactivate" y "Decrease".
Los booleanos, debido al esquema del OSCHandler, también se pasarán como 0 / 1. El mensaje que se manda coincide con el valor
del output (de entre los del enumerado) pero todo en minúscula y con una barra lateral delante. Ej: Reverb-> "/reverb"


### 12/02/20
- Creada una demo de un juego espacial para poner a prueba las tres cosas realizadas por cada uno de los integrantes que son:
	-Primera versión funcional del plugin
	-Correcta conexión y envío de mensajes entre unity y supercollider 
	-Primeras versiones prototípicas de código en supercollider que permiten generar musica procedural y adaptativa.
El juego consiste en una nave que dispara a los 10 segundos una nave enemiga aparece y te dispara y si le das 10 veces se va para 
volver tras pasar 10 segundos.


### 13/02/20
- Añadidos nuevos outputs para la demo de la reunión de hoy: Percussions, BackgroundMusic, AmbienceFX, IntenseFX.
Metido el MusicManager en la escena de la demo satisfactoriamente. (El "usuario" ya no manda mensajes a SuperCollider)

### 14/02/20
- Mejorada la UI del plugin, ya solo hay que arrastrar el GameObject que se desea usar, y a partir de ahi todo lo demás
se selecciona mediante menús desplegables. Los números "min" y "max" solo aparecen si la variable no es booleana
- Falta refactorizar código repetido en MMFoundation

### 27/02/20
- Comenzada la refactorización de código de supercollider. Se acabaron los test aleatorios en archivos sin sentido. A partir de 
ahora estará todo organizado convenientemente para su fácil uso y entendimiento. Aun queda mucho pero estamos en ello

### 01/03/20
- Comenzada la ventana relacionada con la primera parte del plugin en Unity. Te deja elegir paquete, queda guardar la información 
en un sitio no perecedero.

### 08/03/20
- A lo largo de la ultima semana se ha creado un generador de acordes aleatorio en funcion de una nota base y un modo indicados
tambien comenzados varios paquetes de sonido, el de terror por mi parte. 

### 09/03/20
- Se envía un mensaje al cliente de SuperCollider desde Unity al iniciar la escena. En este se indica el paquete elegido
y se deberán añadir también los parámetros opcionales iniciales. Con esto, el cliente debe inicializarse, compilar los scripts
y comenzar a reproducir la música.

### 14/03/20
- Cambiado por completo la estructura del codigo de supercollider, ahora funciona por programación orientada a objetos creando 
las clases pertinentes. Esto permite que no tengamos que complira a mano el código sino que se cree una instancia del MusicMaker
que se encarga de compilarlo y ejecutarlo todo.

- En proceso de poner los nombres de métodos de Unity con UpperCamelCase

### 20/03/20
- Reunión con Jaime para que vea los progreso de los paquetes que se están haciendo
- Empezada demo para el paquete de terror

### 30/03/20
- Reunión por videollamada con Jaime y Marco para que vieran el progreso del proyecto
y para que Marco nos aconsejara sobre el enfoque respecto a la generación de melodías.
- Se decide, por consejo suyo, poner énfasis en los llamados clichés, con la idea de tener una lista de clichés
(con la duración y tono de cada nota) para cada género de forma paralela a los acordes y ritmo que suenan.

### 07/4/20
- Creados los diccionariso base para separar los eventos de los paquetes en supercollider

### 12/04/20
- Reunión sin profesor. Se acuerda seguir una estructura en el código de SuperCollider para unificar la estructura,
métodos y gestión de las capas que tendrá cada uno de los paquetes. 
- Esta estructura distingue 4 partes de la música: base rítmica, base armónica, melodía y OneShots. Cada parte tiene una lista de capas
que pueden sonar a la vez, de forma que puede no sonar (0), tener todas las capas activas (e.g., 1, 2, 3), o cualquier combinación 
posible.
- Paquetes de Desierto, Terror y Ambiente en proceso (el 1º más avanzado). Los siguientes en hacerse 
serán el Acuático y el Electrónico, con la idea de tenerlos todos funcionando bien antes de
detallar y exprimir cada uno al máximo (y considerarlos así "cerrados")

### 13/04/20
- Actualizado la interfaz de Unity con la nueva estructura a seguir. Se muestran las capas
para que el usuario elija cuáles empiezan activas.

### 14/04/20
- Ahora el plugin permite usar elementos que estén en un array para la parte adaptativa, indicando el índice.
Se ha refactorizado el código de "Utilities", moviéndolo casi todo a las propias clases
MusicInput y MusicTuple.
- Reunión con Jaime:

DE POR MEDIO HAY MUCHO TRABAJO REALIZADO SIN BITACORIZAR

### 16/05/20
-He hecho la escena de botones para testing, hay que hacer las llamadas a SC


