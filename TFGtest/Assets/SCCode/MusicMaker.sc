MusicMaker {
	var <params, <server, <effects, <packages, <synths, <messages, <actualPackage;

	init{ arg package;
		var terror, ambient, desert;

		packages = Dictionary.new;

		synths = Synths.new;
		synths.init;

		effects = Effects.new;
		effects.init;

		params = MusicMakerParameters.new;
		params.init(1, 0);

		actualPackage = package;

		server = PluginServer.new;
		server.init;

		server.server.waitForBoot({
			server.server.sync;
			~unityClient = NetAddr.new("127.0.0.1",7771);
			NetAddr.localAddr;
			server.boot = NetAddr.new("127.0.0.1", 7771);
			server.server.sync;

			server.server.sync;
			ServerTree.add(~makeNodes);
			server.server.freeAll;
			server.server.sync;

			server.server.sync;
			terror = TerrorPackage.new;
			terror.init(params);
			ambient = AmbientPackage.new;
			ambient.init(params);
			desert = DesertPackage.new;
			desert.init(params);
			server.server.sync;

			server.server.sync;
			packages.add(\Horror -> terror );
			packages.add(\Ambient -> ambient );
			packages.add(\Desert -> desert );
			server.server.sync;

			server.server.sync;
			messages = Messages.new;
			messages.init(actualPackage);
			server.server.sync;

			server.server.sync;
			//this.start;
			server.server.sync;
			"done".postln;
		});
	}

	start{
		packages[actualPackage].play;
	}

	setPackage{ arg pck;
		actualPackage = pck;
	}
}



