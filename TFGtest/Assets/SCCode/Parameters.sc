//EL NÚMERO DE CAPAS SIEMPRE TIENE QUE SER >= 1
MusicMakerParameters {
	var <tempo = 1, <armonicBase = false,
	<number_of_melodic_layers = 1, <number_of_percs_layers = 1, <rhythmBase = false;

	//TODO: hacer que sea un solo método y cant pueda ser negativo¿?
	//Aument el tampo
	increaseTempo{ arg cant;
		tempo += cant;
	}

	//Disminuye el tempo
	decreaseTempo{ arg cant;
		tempo -= cant;
	}

	//Establece el nº de capas melódicas
	setMelodicLayers{ arg value;
		number_of_melodic_layers = value;
	}

	//Establece el nº de capas de percusión
	setPercLayers{ arg value;
		number_of_percs_layers = value;
	}

	//Activa/desactiva la base rítmica
	setRhythmBase{ arg value;
		rhythmBase = value;
	}

	//Activa/desactiva la base armónica
	setArmonicBase{ arg value;
		armonicBase = value;
	}
}
