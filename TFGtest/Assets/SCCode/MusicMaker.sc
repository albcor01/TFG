
MusicMaker {

	var <params, <server, <effects, <packages, <synths, <messages, <actualPackage;

	init{ arg package;
		var terror, ambient, desert, fantasy;

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
			~unityClient = NetAddr.new("127.0.0.1", 7771);
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
			fantasy = FantasyPackage.new;
			fantasy.init(params);
			server.server.sync;

			server.server.sync;
			packages.add(\Terror -> terror );
			packages.add(\Ambient -> ambient );
			packages.add(\Desert -> desert );
			packages.add(\Fantasy -> fantasy);
			server.server.sync;

			server.server.sync;
			messages = Messages.new;
			messages.init(packages[actualPackage]);
			server.server.sync;

			server.server.sync;
			//this.start;
			server.server.sync;
			"MusicMaker inicializado".postln;
		});
	}

	start{
		packages[actualPackage].playRhythm(0);
	}

	start2{
		packages[actualPackage].playRhythm(1);
	}

	start3{
		packages[actualPackage].playRhythm(2);
	}

	start4{
		packages[actualPackage].playHarmony(0);
	}

	start5{
		packages[actualPackage].playHarmony(1);
	}

	start6{
		packages[actualPackage].playMelody(0);
	}

	start7{
		packages[actualPackage].playMelody(1);
	}

	start8{
		packages[actualPackage].playMelody(2);
	}

	start9{
		packages[actualPackage].playOS(0);
	}

	start10{
		packages[actualPackage].playOS(1);
	}

	stop{
		packages[actualPackage].stopRhythm(0);
	}

	setPackage{ arg pck;
		actualPackage = pck;
	}
}