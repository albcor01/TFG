
TerrorPackage : Package {


	init{ arg parameters;
		percs = Dictionary.new;
		chords = Dictionary.new;
		melodies = Dictionary.new;

		params = parameters;

		melodies.add(\CreepyMelody -> {
		~creepyMelody = Pbind(
			\instrument, \bpfsaw,
			\dur, Pwhite(4.5, 5.0, inf),
			\midinote, Pxrand(~autoChords, inf),
			\detune,1,
			\cfmin, 100,
			\cfmax, 200,
			\rqmin, Pexprand(0.05, 0.05, inf),
			\atk, Pwhite(2.0, 2.5, inf),
			\rel, Pwhite(5, 6.0, inf),
			\ldb, 6,
			\amp, 2,
			\group, ~mainGrp,
			\out, ~bus[\reverb],
		).play;
	});
	melodies.add(\StopCreepyMelody -> {
		~creepyMelody.stop;
	});
	melodies.add(\SoftMelody -> {
		Routine({
			~softMelody = Pbind(
				\instrument, \organDonor,
				\dur, Pwhite(4.5, 5.0, inf),
				\midinote, Pxrand(~autoChords, inf),
				\rqmin, Pexprand(0.05, 0.05, inf),
				\att, 0.01,
				\rel, Pwhite(5, 6.0, inf),
				\ldb, 1,
				\amp, 2,
				\group, ~mainGrp,
				\out, ~bus[\reverb],
				\cutoff, 100,
			).play;
		}).play(AppClock);
	});
	melodies.add(\StopSoftMelody -> {
		~softMelody.stop;
	});

	percs.add(\oneshot1 -> {
		12.do{
			Synth(
				\bpfsaw,
				[
					\atk, exprand(0.5,1.5),
					\rel, exprand(2.0,8.0),
					\c1, exprand(4,10.0),
					\c2, exprand(2.0,5).neg,
					\freq, exprand(8,60),
					\detune, rrand(0.1,4),
					\cfmin, 30,
					\cfmax, 400,
					\rqmin, 0.02,
					\rqmax, 0.08,
					\amp, exprand(0.5,0.9),
					\pan, rrand(-0.5,0.5),
					\out, ~bus[\reverb],
				],
				~mainGrp
			);
		};
	});
	}
}