~effects = {
	SynthDef(\reverb, {
		arg in, predelay=0.1, revtime=1.8,
		lpf=4500, mix=0.15, amp=1, out=0;
		var dry, wet, temp, sig;
		dry = In.ar(in,2);
		temp = In.ar(in,2);
		wet = 0;
		temp = DelayN.ar(temp, 0,2, predelay);
		16.do{
			temp = AllpassN.ar(temp, 0.05, {Rand(0.001,0.05)}!2, revtime);
			temp = LPF.ar(temp, lpf);
			wet = wet + temp;
		};
		sig = XFade2.ar(dry, wet, mix*2-1, amp);
		sig = FreeVerb.ar(sig, 0.5,0.8,0.2)!2;
		Out.ar(out, sig);
	}).add;

	//Recibe una nota en midi y un modo del 0 al 7
	//la nota midi se recomienda que este entre un LA 220 (midinote:57)
	//y un LA 880 (midinote:77) los modos que acepta la función van del
	//0 al 6 siendo estos los siguientes
	//0 = Jonico, 1 = Dorico, 2 = Frigio, 3 = Lidio, 4 = Mixolidio, 5 = Eolico, 6 = Locrio
	//A partir de aquí generara un array con arrays de acordes construidos con notas en el
	//modo elegido a partir de la nota elegida.
	~chorsdGen = {
		arg midi = 57, modo = 0;
		var dist, ran, chords, actualPos, midiNotes, actualMidi;

		dist = Array.with(2,2,1,2,2,2,1); //El array base de distancias de los modos griegos
		actualPos = modo;
		actualMidi = midi;
		ran = rrand(4,10); //Numero de acordes que vamos a crear;


		midiNotes = Array.fill(8,{
			actualMidi = actualMidi+dist[actualPos%7];
			actualPos = actualPos + 1;
			actualMidi;
		});
		midiNotes.insert(0, midi);
		chords = Array.fill(10,{
			r = rrand(2, 5);
			a = Array.fill(r,{
				b = rrand(0,7);
				midiNotes[b];
			});
			a;
		});
		midiNotes.postln;
		chords;
	};

	~autoChords = ~chorsdGen.value(60, 5);
}