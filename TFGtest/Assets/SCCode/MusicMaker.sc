
MusicMaker {
	var <params, <server, <effects, <packages, <synths;

	init{
		var terror, ambient;
		packages = Dictionary.new;

		synths = Synths.new;
		synths.init;

		effects = Effects.new;
		effects.init;

		params = MusicMakerParameters.new;

		terror = TerrorPackage.new;
		terror.init(params);
		ambient = AmbientPackage.new;
		ambient.init(params);

		packages.add(\terror -> terror );
		packages.add(\ambient -> ambient );

		server = PluginServer.new;
		server.init;
	}
}
