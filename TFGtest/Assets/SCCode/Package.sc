
Package { var <percs, <chords, <melodies, <params;
	init{}
	playAll{}
	stopAll{}

	//Recibe el id del estilo de al capa de percusión que sonará. Si esta no esta sonando la activara y la marcará como funcionando. En caso contrario no pasará nada.
	playRhythm{ arg capa;
		switch(capa,
		0, {
			if(params.percs_layers[capa] == false ,{percs[\DesertPercs].value; params.activatePercsLayer(0)});
		},
		1, {

		},
		2, {

		}
		)
	}

	stopRhythm{ arg capa; }

	playHarmony{ arg capa; }
	stopHarmony{ arg capa; }

	playMelody{ arg capa; }
	stopMelody{ arg capa; }

	playOS{ arg capa; }

	multiplyTempo{ arg mult; }
	octaveMelody{ arg oct; }

}