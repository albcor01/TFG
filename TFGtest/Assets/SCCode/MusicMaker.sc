
MusicMaker {
	var <params, <server, <effects, <packages, <synths;

	init{
		var terror, ambient, desert;
		packages = Dictionary.new;

		synths = Synths.new;
		synths.init;

		effects = Effects.new;
		effects.init;

		server = PluginServer.new;
		server.init;

		params = MusicMakerParameters.new;

		terror = TerrorPackage.new;
		terror.init(params);
		ambient = AmbientPackage.new;
		ambient.init(params);
		desert = DesertPackage.new;
		desert.init(params);

		packages.add(\terror -> terror );
		packages.add(\ambient -> ambient );
		packages.add(\desert -> desert );
	}
}

a = MusicMaker.new;
a.init;

// PERCS
a.packages[\desert].test;
a.packages[\desert].test2;
a.packages[\desert].test3;

// SHAKERS
a.packages[\desert].test4;
a.packages[\desert].test5;

// CHORD
a.packages[\desert].test6;
a.packages[\desert].test7;

// MELODY 1
a.packages[\desert].test8;
a.packages[\desert].test9;
a.packages[\desert].test10;

// MELODY 2
a.packages[\desert].test11;
a.packages[\desert].test12;

// MELODY 3
a.packages[\desert].test13;
a.packages[\desert].test14;

// OS 1
a.packages[\desert].test15;
a.packages[\desert].test16;

// OS 2
a.packages[\desert].test17;

a.server.plotTree;