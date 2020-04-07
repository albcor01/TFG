
AmbientPackage : Package {


	init{ arg parameters;

		percs = Dictionary.new;
		chords = Dictionary.new;
		melodies = Dictionary.new;
		params = parameters;

		percs.add(\Drone -> {
		~drone = Pbind(
			\instrument, \bpfsaw,
			\dur, 1,
			\freq, 26.midicps,
			\detune, Pwhite(0.03,0.2),
			\rqmin, 0.08,
			\rqmax, 0.12,
			\cfmin, 50,
			\cfmax, 400,
			\atk, 2,
			\sus, 0.1,
			\rel, 2,
			\amp, 0.8,
			\group, ~mainGrp,
			\out, ~bus[\reverb],
		).play;
	});
	percs.add(\StopDrone -> {
		~drone.stop;
	});

	percs.add(\Bubbles -> {
		Routine({
			~bubbles = Pbind(
				\instrument, \bpfsaw,
				\dur, Pwhite(0.1,0.5),
				\freq, Pexprand(1,25),
				\detune, Pwhite(0.03, 0.2, inf),
				\rqmin, 0.1,
				\rqmax, 0.5,
				\cfmin, 50,
				\cfmax, 2000,
				\atk, 2,
				\sus, 0,
				\rel, Pexprand(3,8),
				\pan, Pwhite(-0.9,0.9),
				\amp, Pexprand(0.05,0.1),
				\group, ~mainGrp,
				\out, ~bus[\reverb],
			).play;
		}).play(AppClock);
	});
	percs.add(\StopBubbles -> {
		~bubbles.stop;
	});

	melodies.add(\Marimba -> {
		~simpleMarimba = Pbind(
			\instrument, \bpfsaw,
			\dur, Prand([0.5,1,2,3],inf),
			\freq, Prand([1/2,3/4,1,3/2,2], inf),
			\detune, Pwhite(0,0.002),
			\rqmin, 0.005,
			\rqmax, 0.008,
			\cfmin, Prand([61,71,78,85,95].midicps,inf),
			\cfmax, Pkey(\cfmin) * Pwhite(1.008,1.025,inf),
			\atk, 3,
			\sus, 1,
			\rel, 5,
			\amp, 1,
			\group, ~mainGrp,
			\out, ~bus[\reverb],
		).play;
	});
	melodies.add(\StopMarimba -> {
		~simpleMarimba.stop;
	});

	melodies.add(\AmbientMelody -> {
		~chords = Pbind(
			\instrument, \bpfsaw,
			\dur, Pwhite(4.5,7.0),
			\midinote, Pxrand([
				[23,35,54,63,64],
				[45,52,54,59,61,64],
				[28,40,47,56,59,63],
				[42,52,57,61,63]
			], inf),
			\detune, Pexprand(0.05,0.2),
			\cfmin, 100,
			\cfmax, 1500,
			\rqmin, Pexprand(0.01,0.15),
			\atk, Pwhite(2.0,2.5),
			\rel, Pwhite(6.5,10.0),
			\ldb, 6,
			\amp, 0.2,
			\group, ~mainGrp,
			\out, ~bus[\reverb],
		).play;

		~marimba = Pbind(
			\instrument, \bpfsaw,
			\dur, Prand([1,0.5],inf),
			\freq, Prand([1/2,2/3,1,4/3,2,5/2,3,4,6,8],inf),
			\detune, Pwhite(0,0.1),
			\rqmin, 0.005,
			\rqmax, 0.008,
			\cfmin, Prand((Scale.major.degrees+64).midicps,inf) * Prand([0.5,1,2,4],inf),
			\cfmax, Pkey(\cfmin) * Pwhite(1.008,1.025),
			\atk, 3,
			\sus, 1,
			\rel, 5,
			\amp, 1,
			\group, ~mainGrp,
			\out, ~bus[\reverb],
		).play;
	});

	melodies.add(\StopAmbientMelody -> {
		~marimba.stop;
		~chords.stop;
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