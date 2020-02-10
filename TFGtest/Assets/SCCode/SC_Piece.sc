// PIECE EXAMPLE

// SAW SYNTH
// ---------------------------------------------------
(
SynthDef(\bpfsaw, {
	arg atk = 2, sus = 0, rel = 3, c1 = 1, c2 = (-1),
	freq = 500, detune = 0.2, pan = 0,
	cfhzmin = 0.1, cfhzmax = 0.3,
	cfmin = 500, cfmax = 2000,
	rqmin = 0.1, rqmax = 0.2,
	amp = 1, out = 0,
	lsf = 200, ldb = 0;
	var sig, env;
	env = EnvGen.kr(Env([0, 1, 1, 0], [atk, sus, rel], [c1, 0, c2]), doneAction:2);
	sig = Saw.ar(freq * {LFNoise1.kr(0.5, detune).midiratio}!2);
	sig = BPF.ar(
		sig,
		{LFNoise1.kr(LFNoise1.kr(4).exprange(cfhzmin, cfhzmax)).exprange(cfmin, cfmax)}!2,
		{LFNoise1.kr(0.1).exprange(rqmin, rqmax)}!2
	);
	sig = BLowShelf.ar(sig, lsf, 0.5, ldb);
	sig = Balance2.ar(sig[0], sig[1], pan);
	sig = sig * env * amp;
	Out.ar(out, sig);
}).add;
)

// NOTE GEN
// ---------------------------------------------------
(
Synth.new(\bpfsaw, [\freq, 440, \amp, 0.7]);
)

// CHORD GEN
// ---------------------------------------------------
(
(3..6).choose.do{
	Synth(
		\bpfsaw,
		[
			\freq, (Scale.minor.degrees+60).midicps.choose,
			\detune, 0.2,
		]
	);
};
)

// BUBBLE GEN
// ---------------------------------------------------
(
10.do{
	Synth(
		\bpfsaw,
		[
			\freq, 50,
			\amp, 0.7,
			\cfmin, 50*2,
			\cfmax, 50*50,
			\rqmin, 0.005,
			\rqmax, 0.03,
			\detune, 0,
			\pan, 0,
			\cfhzmin, 5,
			\cfhzmax, 40
		],
	);
};
)

// PROG GEN
// ---------------------------------------------------
(
~chords = Pbind(
	\instrument, \bpfsaw,
	\dur, Pwhite(4.5, 7.0, inf),
	\midinote, Pxrand([
		[23, 35, 54, 63, 64],
		[45, 52, 54, 59, 61, 64],
		[28, 40, 47, 56, 59, 63],
		[42, 52, 57, 61, 63],
	], inf),
	\detune, Pexprand(0.05, 0.2, inf),
	\cfmin, 100,
	\cfmax, 1500,
	\rqmin, Pexprand(0.01, 0.15, inf),
	\atk, Pwhite(2.0, 2.5, inf),
	\rel, Pwhite(6.5, 10.0, inf),
	\ldb, 6,
	\amp, 0.6,
	\out, 0,
).play;
)

// EDIT PROG
// ---------------------------------------------------
(
~chords.stream = Pbind(
	\instrument, \bpfsaw,
	\dur, Pwhite(5.0, 7.0, inf),
	\midinote, Pxrand([
		[22, 33, 52, 61, 62],
		[43, 50, 52, 57, 59, 62],
		[26, 38, 45, 54, 57, 61],
		[40, 50, 55, 59, 61],
	], inf),
	\detune, Pexprand(0.05, 0.15, inf),
	\cfmin, 100,
	\cfmax, 1500,
	\rqmin, Pexprand(0.01, 0.15, inf),
	\atk, Pwhite(2.0, 2.5, inf),
	\rel, Pwhite(6.5, 10.0, inf),
	\ldb, 6,
	\amp, 0.5,
	\out, 0,
).asStream;
)

~chords.stop;

// MARIMBA GEN
// ---------------------------------------------------
(
~marimba = Pbind(
	\instrument, \bpfsaw,
	\dur, Prand([1, 0.5], inf),
	\freq, Prand([1/2, 2/3, 1, 4/3, 2, 5/2, 3, 4, 6, 8], inf),
	\detune, Pwhite(0, 0.1, inf),
	\rqmin, 0.005,
	\rqmax, 0.008,
	\cfmin, Prand((Scale.major.degrees + 64).midicps, inf) * Prand([0.5, 1, 2, 4], inf),
	\cfmax, Pkey(\cfmin) * Pwhite(1.008, 1.025, inf),
	\atk, 3,
	\sus, 1,
	\rel, 5,
	\amp, 0.7,
	\out, 0,
).play;
)

~marimba.stop;