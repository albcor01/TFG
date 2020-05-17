
TerrorPackage : Package {

	    //Variables de la clase

		init {

		arg parameters;

		percs = Dictionary.new;
		chords = Dictionary.new;
		melodies = Dictionary.new;
		oneShots = Dictionary.new;

		params = parameters;

		percs.add(\BasePercs -> {
			Pdef(
				\rhythm,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/2], inf),
					\stretch, 3, // 80bpm -> 80/60bps -> 60/80 -> 60/80*4spc = 3
					\buf, Pseq(
						[
							Prand(~buff[\percs_kicks_high], 1),
							Prand(~buff[\percs_kicks_low], 1),
						], inf
					),
					\amp, Pseq([1.4, Pexprand(0.5, 0.7, 7)], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:3);
		});

		percs.add(\ReBasePercs -> {
			Pdef(
				\rhythm,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/2], inf),
					\stretch, 3, // 80bpm -> 80/60bps -> 60/80 -> 60/80*4spc = 3
					\buf, Pseq(
						[
							Prand(~buff[\percs_kicks_high], 1),
							Prand(~buff[\percs_kicks_low], 1),
						], inf
					),
					\amp, Pseq([1.4, Pexprand(0.5, 0.7, 7)], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).quant_(3);
		});

		percs.add(\StopBasePercs -> {
			Pdef(\rhythm).stop;
		});

		percs.add(\ExtraPercs -> {
			Pdef(
				\rhythm2,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/4], inf),
					\stretch, 3, // 80bpm -> 80/60bps -> 60/80 -> 60/80*4spc = 3
					\buf, Pseq(
						[
							~buff[\percs_bigbells][0],
							~buff[\percs_bigbells][1],
							~buff[\percs_bigbells][0],
							~buff[\percs_bigbells][0],
						], inf
					),
					\amp, 2,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:3);
		});

		percs.add(\ReExtraPercs -> {
			Pdef(
				\rhythm2,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/4], inf),
					\stretch, 3, // 80bpm -> 80/60bps -> 60/80 -> 60/80*4spc = 3
					\buf, Pseq(
						[
							~buff[\percs_bigbells][0],
							~buff[\percs_bigbells][1],
							~buff[\percs_bigbells][0],
							~buff[\percs_bigbells][0],
						], inf
					),
					\amp, 2,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).quant_(3);
		});

		percs.add(\StopExtraPercs -> {
			Pdef(\rhythm2).stop;
		});

		percs.add(\EffectsPercs -> {
			Pdef(
				\rhythm3,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/4], inf),
					\stretch, 3, // 80bpm -> 80/60bps -> 60/80 -> 60/80*4spc = 3
					\buf, Pseq(
						[
							~buff[\percs_bigbells][2],
							~buff[\percs_bigbells][0],
							~buff[\percs_bigbells][0],
							~buff[\percs_bigbells][0],
						], inf
					),
					\amp, 2.75,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:3);
		});

		percs.add(\ReEffectsPercs -> {
			Pdef(
				\rhythm3,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/4], inf),
					\stretch, 3, // 80bpm -> 80/60bps -> 60/80 -> 60/80*4spc = 3
					\buf, Pseq(
						[
							~buff[\percs_bigbells][2],
							~buff[\percs_bigbells][0],
							~buff[\percs_bigbells][0],
							~buff[\percs_bigbells][0],
						], inf
					),
					\amp, 2.75,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).quant_(3);
		});

		percs.add(\StopEffectsPercs -> {
			Pdef(\rhythm3).stop;
		});


		chords.add(\BaseHarmony -> {
			~creepyHarmony = Pbind(
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

		chords.add(\ReBaseHarmony -> {

		});

		chords.add(\StopBaseHarmony -> {
			~creepyHarmony.stop;
		});

		chords.add(\ExtraHarmony -> {
			~softChords = Pbind(
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
		});

		chords.add(\ReExtraHarmony -> {

		});

		chords.add(\StopExtraHarmony -> {
			~softChords.stop;
		});


		melodies.add(\FirstMelody -> {

		});

		melodies.add(\ReFirstMelody -> {

		});

		melodies.add(\StopFirstMelody -> {

		});

		melodies.add(\SecondMelody -> {

		});

		melodies.add(\ReSecondMelody -> {

		});

		melodies.add(\StopSecondMelody -> {

		});

		melodies.add(\ThirdMelody -> {

		});

		melodies.add(\ReThirdMelody -> {

		});

		melodies.add(\StopThirdMelody -> {

		});


		oneShots.add(\FirstOS -> {
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
						\amp, exprand(4.5,6.9),
						\pan, rrand(-0.5,0.5),
						\out, ~bus[\reverb],
					],
					~mainGrp
				);
			};
		});

		oneShots.add(\SecondOS -> {
			1.do{
				Synth(
					\bpfbuf,
					[
						\buf, ~buff[\sfx_terror_strings][0].bufnum,
						\amp, exprand(5.5,8.5),
						\pan, rrand(-0.9,0.9),
						\out, ~bus[\reverb],
					],
					~mainGrp
				);
			};
		});

		oneShots.add(\ThirdOS -> {
			1.do{
				Synth(
					\bpfbuf,
					[
						\buf, ~buff[\sfx_terror_strings][1].bufnum,
						\amp, exprand(3.5,6.5),
						\pan, rrand(-0.9,0.9),
						\out, ~bus[\reverb],
					],
					~mainGrp
				);
			};
		});

		oneShots.add(\FourthOS -> {

		});

		oneShots.add(\FifthOS -> {

		});
	}
}