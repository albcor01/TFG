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

## 02/02/20
- Se cambia el modelo con el que funciona el plugin en Unity; en vez de una ventana del editor, se presenta como un 
script que deberá llevar un GameObject vacío y donde se podrán configurar las tuplas {input-efecto-output} para 
la música adaptativa. De esta forma la organización interna es mucho más sencilla y la persistencia se 
aprovecha de la escena de Unity sin necesidad de guardar datos a ningún fichero.
- Se eliminan por tanto algunos Scripts innecesarios (PluginTFG y MMManager)

##04/02/20
- Se consigue que solo se manden los mensajes a SuperCollider en el caso de que las variables de entrada
hayan modificado su valor. Empezada distinción de casos para procesar los distintos tipos de mensaje.
- Se crea "MMTest.sc", un archivo de SuperCollider parecido al "Events(Test).sc" para hacer pruebas 
de comunicación desde Unity. NOTA: el script OSCCall ya no es necesario, lo único que hacía se hace ahora en 
el "Start()" de MusicMaker.cs
- Interfaz básica con un par de botones en MMTest.unity

##05/02/20
Se ha conseguido que los mensajes que llegan de supercollider a unity sean coherentes. Antes los mensaje llegaban 
de manera que se añadian a una lista y nunca se iban además se volcaba el ultimo paquete recibido en cada tick porque
nunca volvia a ser null ESTO HABRIA QUE MIRAR SI SE ACTUALIZA EN CADA TICK O SOLO CUANDO LLEGA NUEVA INFORMACION PORQUE 
ENTONCES CAMBIA. habiendo realizado cambios en el codigo de OSCBundle he conseguido definitivamente que los datos sean coherentes.