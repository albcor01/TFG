MusicMaker {
	var <params, <server, <effects, <packages, <synths, <messages, <actualPackage;

	init{ arg package;
		var terror, ambient, desert;

		//Inicializamos las variables
		packages = Dictionary.new; //diccionario de paquetes

		synths = Synths.new; //sintetizadores
		synths.init;

		effects = Effects.new;//efectos
		effects.init;

		params = MusicMakerParameters.new; //parámetros de la música activa

		actualPackage = package; //paquete en ejecución

		server = PluginServer.new; //servidor de SC
		server.init;

		//Esperamos a que el server se arranque
		server.server.waitForBoot
		({
			//Dirección del cliente de Unity (loopback)
			server.server.sync;
			~unityClient = NetAddr.new("127.0.0.1",7771);
			NetAddr.localAddr;
			server.boot = NetAddr.new("127.0.0.1", 7771);
			server.server.sync;

			//Liberamos los nodos
			server.server.sync;
			ServerTree.add(~makeNodes);
			server.server.freeAll;
			server.server.sync;

			//Creamos los paquetes y los inicializamos con los parámetros dados
			server.server.sync;
			terror = TerrorPackage.new;
			terror.init(params);
			ambient = AmbientPackage.new;
			ambient.init(params);
			desert = DesertPackage.new;
			desert.init(params);
			server.server.sync;

			//Una vez creados, los añadimos al diccionario de paquetes
			server.server.sync;
			packages.add(\Terror -> terror );
			packages.add(\Ambient -> ambient );
			packages.add(\Desert -> desert );
			server.server.sync;

			//Creamos la instancia de los mensajes
			server.server.sync;
			messages = Messages.new;
			messages.init(actualPackage);
			server.server.sync;

			//Llamamos al start propio
			server.server.sync;
			this.start;
			server.server.sync;
			"done".postln;
		});
	}

	//Empieza a reproducir la música del paquete actual
	start{
		packages[actualPackage].play;
	}

	//Cambia el paquete (puede hacerlo en tiempo de ejecución)
	setPackage{ arg pck;
		actualPackage = pck;
	}
}



