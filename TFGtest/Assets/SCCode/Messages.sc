//Lista de mensajes que podemos recibir de Unity y acción de cada uno
Messages {
	var params, package;

	init{ var par, pack;

		params = par;
		package = pack;

		OSCdef.new(\Play,{package.play;},'/Play', nil, 57120);
		OSCdef.new(\Stop,{package.stop;},'/Stop', nil, 57120);
	}
}
