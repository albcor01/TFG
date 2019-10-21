//--TEST INIT--
(
s.boot;
Environment.clear;

~unityClient = NetAddr.new("127.0.0.1",7771);
NetAddr.localAddr;

t = TempoClock.new;
t.tempo = 1.5;

b = NetAddr.new("127.0.0.1", 7771);
)

//-- TEST EVENTS--
(
//PIANO TEST PROGRESSION
OSCdef.new(
	\test,
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
	'/test', nil, 57120
);

//PERC TEST BACKGROUND
OSCdef.new(
	\test2,
	{
		//var base;

		~base = Pbind(\amp, 0.8);

		p = Ppar([
			Pbindf(
				~base,
				\instrument, Pseq([\kick, \snare], inf)
			),
			Pbindf(
				~base,
				\instrument, Pseq([Pn(\hihat, 16), Pn(\clap, 16)], inf)
			)
		]).play(t);
	},
	'/test2', nil, 57120
);

//BASIC TEST OSCILATOR
OSCdef.new(
	\test3,
	{
		y = {SinOsc.ar};
	},
	'/test3', nil, 57120
);

//STOP PERC TEST BACKGROUND
OSCdef.new(
	\test4,
	{
		p.free;
		//y.free;
	},
	'/test4', nil, 57120
);

//UP TEMPO TEST
OSCdef.new(
	\test5,
	{
		t.tempo = t.tempo + 0.5;
		b.sendMsg("tempo", t.tempo);
	},
	'/test5', nil, 57120
);

//DEBUG TEST FOR MESSAGES
OSCdef.new(
	\test6,
	{
		y = List.new(3);
		5.do({ arg i; y.add(i); });
		b.sendMsg("/list", y);
	},
	'/test6', nil, 57120
);

//STOP PIANO TEST PROGRESSION
OSCdef.new(
	\test7,
	{
		m.stop;
	},
	'/test7', nil, 57120
);
)