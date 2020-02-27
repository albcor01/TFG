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
-Creada una demo de un juego espacial para poner a prueba las tres cosas realizadas por cada uno de los integrantes que son:
	-primera versión funcional del plugin
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

### 27/02/2020
- Comenzada la refactorización de código de supercollider. Se acabaron los test aleatorios en archivos sin sentido. A partir de 
ahora estará todo organizado convenientemente para su fácil uso y entendimiento. Aun queda mucho pero estamos en ello