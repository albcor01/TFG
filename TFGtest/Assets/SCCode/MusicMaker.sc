
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
