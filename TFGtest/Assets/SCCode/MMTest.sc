//-- Eventos de test --
(

//Arrancar el server
OSCdef.new(
	\boot,
	{
		s.boot;
		Environment.clear;

		~unityClient = NetAddr.new("127.0.0.1",7771);
		NetAddr.localAddr;

		t = TempoClock.new;
		t.tempo = 1;

		b = NetAddr.new("127.0.0.1", 7771);
	},
	'/boot', nil, 57120
);

//Apagar el server
OSCdef.new(
	\quit,
	{
		s.quit;
	},
	'/quit', nil, 57120
);

//Progresión con piano
OSCdef.new(
	\play,
	{
		arg msg; [msg].postln;
		//Synth(\kalimba, [\freq, 440] );
		//Pbind(\instrument, \fmbass, \freq, Prand([1, 1.2, 2, 2.5, 3, 4], inf) * 200).play(TempoClock(2.3));
		//Pbind(\instrument, \fmbass, \degree, Pseq([1, 3, 5, 7], inf), \dur, 2, \octave, 4, \root, 3).play(TempoClock(2.3));
		m = Pbind(
			\instrument, \rhodey_sc,
			\scale, Scale.mixolydian,
			\octave, 4,
			\root, 2,
			\legato, Pseq([0.9, 0.5, 0.5, 0.9, 0.9, 0.9, 0.9, 0.5, 1, 0.5, 1, 0.6, 0.3], inf),
			\dur, Pseq([1 + (1/3), 1/3, 1/3, 1/7, 6/7, 5/6, 1/6, 1/2, 2/6, 1/6, 2 + 1/2, 1, 1/2], inf),
			\degree, Pseq([
				[0, 2, 4], 2, 4, 7, 8, 7, 0, [1, 3, 6], 5, [1, 3, 6], Rest(), [-1, 1, 3], [1, 3, 5],
				[0, 2, 4], 2, 4, 8, 9, 7, 0, [1, 3, 6], 5, [1, 3, 6], Rest(), [-1, 1, 3], [1, 3, 5],
			], inf),
			\mix, 0.2,
			\modIndex, 0.2,
			\lfoSpeed, 0.5,
			\lfoDepth, 0.4,
			\vel, Pgauss(0.8, 0.1, inf),
			\amp, 0.3
		).play(t);
		b.sendMsg("/SCReciever", t.tempo);
	},
	'/play', nil, 57120
);

//Parar la música de piano
OSCdef.new(
	\stop,
	{
		m.stop;
	},
	'/stop', nil, 57120
);

//Cambiar el tempo
OSCdef.new(
	\tempo,
	{
		arg msg;
		//t.tempo = t.tempo + msg[1];
		t.tempo = msg[1];
		b.sendMsg("tempo", t.tempo);
	},
	'/tempo', nil, 57120
);

//Debugear los mensajes
OSCdef.new(
	\debug,
	{
		y = List.new(3);
		5.do({ arg i; y.add(i); });
		b.sendMsg("/list", y);
	},
	'/debug', nil, 57120
);
)