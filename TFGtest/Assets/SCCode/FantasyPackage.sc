
FantasyPackage : Package {

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
					\dur, Pseq([1/6], inf),
					\stretch, 1.2857143, // 140bpm -> 140/60bps -> 60/140spb -> 60/140*3spc = 1.2857
					\buf, Pseq(
						[
							Prand(~buff[\percs_forest_low], 1),
							~buff[\percs_forest_high][0],
							~buff[\percs_forest_high][0],
							~buff[\percs_forest_high][1],
							~buff[\percs_forest_high][1],
							~buff[\percs_forest_high][2],
						], inf
					),
					\amp, Pseq([1.4, Pexprand(0.5, 0.7, 7)], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.2857143);
		});

		percs.add(\ReBasePercs -> {
			Pdef(
				\rhythm,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/3], inf),
					\stretch, 1.2857143, // 140bpm -> 140/60bps -> 60/140spb -> 60/140*3spc = 1.2857
					\buf, Pseq(
						[
							Prand(~buff[\percs_forest_low], 1),
							Prand(~buff[\percs_forest_high], 5),
						], inf
					),
					\amp, Pseq([1.4, Pexprand(0.5, 0.7, 7)], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).quant_(1.2857143);
		});

		percs.add(\StopBasePercs -> {
			Pdef(\rhythm).stop;
		});

		percs.add(\ExtraPercs -> {
			Pdef(
				\rhythm2,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/1], inf),
					\stretch, 1.2857143,
					\buf, Pseq(
						[
							~buff[\percs_forest_esp][0],
							~buff[\percs_forest_esp][1],
						], inf
					),
					\amp, 1,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.2857143);
		});

		percs.add(\ReExtraPercs -> {
			Pdef(
				\rhythm2,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/3], inf),
					\stretch, 1.2857143,
					\buf, Pseq(
						[
							~buff[\percs_forest_esp][0],
							~buff[\percs_forest_esp][1],
						], inf
					),
					\amp, 1,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).quant_(1.2857143);
		});

		percs.add(\StopExtraPercs -> {
			Pdef(\rhythm2).stop;
		});

		percs.add(\EffectsPercs -> {
			Pdef(
				\rhythm3,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/6], inf),
					\stretch, 1.2857143,
					\buf, Pseq(
						[
							~buff[\percs_jinglebells][1],
							~buff[\percs_jinglebells][0],
							~buff[\percs_jinglebells][0],
							~buff[\percs_jinglebells][0],
							~buff[\percs_jinglebells][0],
							~buff[\percs_jinglebells][0],
						], inf
					),
					\amp, Pseq([1.4, Pexprand(0.04, 0.1, 7)], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.2857143);
		});

		percs.add(\ReEffectsPercs -> {
			Pdef(
				\rhythm3,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/3], inf),
					\stretch, 1.2857143,
					\buf, Pseq(
						[
							Prand(~buff[\percs_jinglebells], 6),
						], inf
					),
					\amp, Pseq([1.4, Pexprand(0.04, 0.1, 7)], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).quant_(1.2857143);
		});

		percs.add(\StopEffectsPercs -> {
			Pdef(\rhythm3).stop;
		});


		chords.add(\BaseHarmony -> {
			Pdef(
				\chord,
				Pbind(
					\instrument, \bpfsaw,
					\dur, 5.0,
					\midinote, Pxrand([
						[60, 66, 67]
					], inf),
					\detune, Pexprand(0.05,0.1),
					\cfmin, 100,
					\cfmax, 1000,
					\rqmin, Pexprand(0.01,0.15),
					\atk, Pwhite(1.5,2.5),
					\rel, Pwhite(6.5,10.0),
					\ldb, 6,
					\amp, 0.7,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.2857143);
		});

		chords.add(\ReBaseHarmony -> {

		});

		chords.add(\StopBaseHarmony -> {
			Pdef(\chord).stop;
		});

		chords.add(\ExtraHarmony -> {
			Pdef(
				\chord2,
				Pbind(
					\instrument, \bpfsaw,
					\dur, 4.0,
					\midinote, Pxrand([
						[72, 78]
					], inf),
					\detune, Pexprand(0.05,0.1),
					\cfmin, 100,
					\cfmax, 800,
					\rqmin, Pexprand(0.01,0.15),
					\atk, Pwhite(1.5,2.5),
					\rel, Pwhite(6.5,10.0),
					\ldb, 6,
					\amp, 0.5,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.2857143);
		});

		chords.add(\ReExtraHarmony -> {

		});

		chords.add(\StopExtraHarmony -> {
			Pdef(\chord2).stop;
		});


		melodies.add(\FirstMelody -> {
			Pdef(
				\melodies,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/1], inf),
					\stretch, 1.2857143,
					\buf, Pseq(
						[
							Pxrand(~buff[\scale_lydian], 1),
						], inf
					),
					\rel, Pwhite(8.0),
					\amp, Pseq([3, 4.5], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.2857143);
		});

		melodies.add(\ReFirstMelody -> {
			Pdef(
				\melodies,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/2], inf),
					\stretch, 1.2857143,
					\buf, Pseq(
						[
							Pxrand(~buff[\scale_lydian], 1),
						], inf
					),
					\rel, Pwhite(8.0),
					\amp, Pseq([3, 4.5], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).quant_(1.2857143);
		});

		melodies.add(\StopFirstMelody -> {
			Pdef(\melodies).stop;
		});

		melodies.add(\SecondMelody -> {
			//cliches
		});

		melodies.add(\ReSecondMelody -> {

		});

		melodies.add(\StopSecondMelody -> {

		});

		melodies.add(\ThirdMelody -> {
			//algo con los shorts
		});

		melodies.add(\ReThirdMelody -> {

		});

		melodies.add(\StopThirdMelody -> {

		});


		oneShots.add(\FirstOS -> {
			1.do{
				Synth(
					\bpfbuf,
					[
						\buf, ~buff[\sfx_crickets][0].bufnum,
						\amp, exprand(2.2,4.5),
						\pan, rrand(-0.9,0.9),
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
						\buf, ~buff[\sfx_wind][0].bufnum,
						\amp, exprand(2.2,4.5),
						\pan, rrand(-0.9,0.9),
						\out, ~bus[\reverb],
					],
					~mainGrp
				);
			};
		});

		oneShots.add(\ThirdOS -> {

		});

		oneShots.add(\FourthOS -> {

		});

		oneShots.add(\FifthOS -> {

		});
	}
}