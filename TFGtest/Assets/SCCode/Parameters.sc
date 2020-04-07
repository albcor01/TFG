//EL NÚMERO DE CAPAS SIEMPRE TIENE QUE SER 1 o MAYOR
MusicMakerParameters {
	var <tempo = 1, <armonicBase = false,
	<number_of_melodic_layers = 1, <number_of_percs_layers = 1, <rhythmBase = false;

	increaseTempo{ arg cant;
		tempo += cant;
	}

	decreaseTempo{ arg cant;
		tempo -= cant;
	}

	setMelodicLayers{ arg value;
		number_of_melodic_layers = value;
	}

		setPercLayers{ arg value;
		number_of_percs_layers = value;
	}

	setRhythmBase{ arg value;
		rhythmBase = value;
	}

	setArmonicBase{ arg value;
		armonicBase = value;
	}
}
