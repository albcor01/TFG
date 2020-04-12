1 - Para que todos los metodos sean comunes deben llamarse igual

2 - Es posible llamar desde packages.sc a una funcion (Ej - PlayMelody) y que llame a la de la clase
	que la herede? (Ej - DesertPackage)

3 - Si lo de arriba es posible -> Definir las funciones en la clase Package
    Si lo de arriba no es posible -> Definir cada funcion en su propia clase

4 - Idea de estructura: 

	Que todos los paquetes ofrezcan lo mismo, esto para el usuario va a resultar super intuitivo.

	De esta manera siempre va a poder hacer las mismas acciones, de tal manera que va a poder programar
	el uso de la musica independientemente del paquete y hasta cambiarlo en tiempo real de tal manera
	que cuando se cambia de paquete cambia todo el sonido pero las funcionalidades no.

	La funcionalidad basica quedaria asi (ir añadiendo opciones):

		- Base ritmica: Funcionamiento por capas y flags para controlar cual esta sonando y cual no. El objetivo esta en
		  definir un numero de capas concreto que queramos de percusion (de 0 a 3). Ejemplo: Desierto -> 0 - Nada, 1 - Congas
		  2 - Capa extra de golpes a Djembe, 3 - Shackers

		- Base armonica: Funcionamiento por capas y flags para controlar cual esta sonando y cual no. Lo mismo que con las percusiones
		  (de 0 a 2). Ejemplo: Ambiente -> 0 - Nada, 1 - Progresion ambiental, 2 - Acorde octavado mantenido

		- Base melodica: Exactamente igual que los anteriores (de 0 a 3). Ejemplo: Desierto -> 0 - Nada, 1 - Melodia random, 
		  2 - Melodias predefinidas 1, 3 - Melodias predefinidas 2

		- One shots: De 0 a 4, cada paquete tendra los suyos propios dentro del contexto. 

		--- Los metodos que se definan afectaran de una manera u otra a cualquiera de estos apartados de arriba, son todos comunes
		--- entre los paquetes para que pueda ser funcionalidad generica al plugin.

		-> Metodos: 

			playRhythm(capa)
			stopRhythm(capa)

			playHarmony(capa)
			stopHarmony(capa)
			
			playMelody(capa)
			stopMelody(capa)
			
			playOS(capa)
			stopOS(capa) - No deberia ser necesario

			multiplyTempo(multiplicador) - Multi > 0.5 && Multi < 1.5

			octavateMelody(multiplicador) - Multi == -1 || Multi == 0 || Multi == 1

	Para que esto funcione guay estructuraremos los buffers de la siguiente manera en carpetas:

		- Percs: Contendrá dentro todo lo relativo a las percusiones (cada paquete lo organizará como sea conveniente)
		  (Ej: buffers\percs\dessert\africanPercsHigh)

		- Scales: A partir de las cuales cada paquete construirá su música (toda la musica que no se haga a partir de sintes de sc)
		  (Ej: buffers\scales\terror\minorScale)

		- SFX: Efectos de sonido para los One Shots de cada paquete.
		  (Ej: buffers\sfx\ambient\bells)

		Cada paquete tendrá varios tipos y timbres de percusiones, escalas y sfx para asegurar que no suene todo el rato lo mismo. 