
DesertPackage : Package {

	    //Variables de la clase

		init{ arg parameters;

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
					\dur, Pseq([1/8], inf),
					\stretch, 1.875, // 128bpm -> 128/60bps -> 60/128spb -> 60/128*4spc = 1.875
					\buf, Pseq(
						[
							Prand(~buff[\africanPercsLow], 1),
							Prand(~buff[\africanPercsHigh], 7),
							Prand(~buff[\africanPercsLow], 1),
							Prand(~buff[\africanPercsHigh], 7),
						], inf
					),
					\amp, Pseq([1.4, Pexprand(0.4, 0.8, 7)], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.875);
		});

		percs.add(\ReBasePercs -> {
			Pdef(
				\rhythm,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/16], inf),
					\stretch, 1.875, // 128bpm -> 128/60bps -> 60/128spb -> 60/128*4spc = 1.875
					\buf, Pseq(
						[
							Prand(~buff[\africanPercsLow], 1),
							Prand(~buff[\africanPercsHigh], 7),
							Prand(~buff[\africanPercsLow], 1),
							Prand(~buff[\africanPercsHigh], 7),
						], inf
					),
					\amp, Pseq([1.4, Pexprand(0.4, 0.8, 7)], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).quant_(1.875);
		});

		percs.add(\StopBasePercs -> {
			Pdef(\rhythm).stop;
		});

		percs.add(\ExtraPercs -> {

		});

		percs.add(\ReExtraPercs -> {

		});

		percs.add(\StopExtraPercs -> {

		});

		percs.add(\EffectsPercs -> {
			~shakerSustain = Pbind(
				\instrument, \bpfbuf,
				\dur, Pwhite(0.2,0.7),
				\atk, Pexprand(2,4),
				\rel, Pexprand(3,5),
				\buf, ~buff[\shakers][13].bufnum,
				\rate, Pwhite(-7.0,-4.0).midiratio,
				\spos, Pwhite(0, ~buff[\shakers][13].numFrames/2),
				\amp, Pexprand(1.0,3.0),
				\freq, {rrand(85.0,105.0).midicps}!3,
				\rq, 0.005,
				\bpfmix, 0.97,
				\group, ~mainGrp,
				\out, ~bus[\reverb],
			).play;
		});

		percs.add(\ReEffectsPercs -> {

		});

		percs.add(\StopEffectsPercs -> {
			~shakerSustain.stop;
		});

		chords.add(\BaseHarmony -> {
			Pdef(
				\chord,
				Pbind(
					\instrument, \bpfsaw,
					\dur, 5.0,
					\midinote, Pxrand([
						[60, 64, 67]
					], inf),
					\detune, Pexprand(0.05,0.1),
					\cfmin, 100,
					\cfmax, 1500,
					\rqmin, Pexprand(0.01,0.15),
					\atk, Pwhite(2.0,2.5),
					\rel, Pwhite(6.5,10.0),
					\ldb, 6,
					\amp, 0.8,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.875);
		});

		chords.add(\ReBaseHarmony -> {

		});

		chords.add(\StopBaseHarmony -> {
			Pdef(\chord).stop;
		});

		chords.add(\ExtraHarmony -> {

		});

		chords.add(\ReExtraHarmony -> {

		});

		chords.add(\StopExtraHarmony -> {

		});

		melodies.add(\FirstMelody -> {
			Pdef(
				\melodies,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/1], inf),
					\stretch, 1.875, // 128bpm -> 128/60bps -> 60/128spb -> 60/128*4spc = 1.875
					\buf, Pseq(
						[
							Pxrand(~buff[\doubleHarmonicScale], 1),
						], inf
					),
					\rel, Pwhite(10.0),
					\amp, Pseq([3.8, 5.0], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.875);
		});

		melodies.add(\ReFirstMelody -> {
			Pdef(
				\melodies,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pseq([1/2], inf),
					\stretch, 1.875, // 128bpm -> 128/60bps -> 60/128spb -> 60/128*4spc = 1.875
					\buf, Pseq(
						[
							Pxrand(~buff[\doubleHarmonicScale], 1),
						], inf
					),
					\rel, Pwhite(10.0),
					\amp, Pseq([3.8, 5.0], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).quant_(1.875);
		});

		melodies.add(\StopFirstMelody -> {
			Pdef(\melodies).stop;
		});

		melodies.add(\SecondMelody -> {
			Pdef(
				\melodies2,
				Pbind(
					\instrument, \bpfsaw,
					\dur, Pwhite(0.5,5.0),
					\midinote, Pxrand
					(
						[
							Pseq([[72],[73],[76],[77],[79],[80],[82]], 1),
							Pseq([[73],[72],[73],[77],[79],[80],[82]], 1),
							Pseq([[82],[80],[79],[77],[79],[80],[82]], 1),
							Pseq([[72]], 7),
							Pseq([[72]], 4),
						],
						inf
					),
					\detune, Pexprand(0.05,0.2),
					\cfmin, 100,
					\cfmax, 1500,
					\rqmin, Pexprand(0.01,0.15),
					\atk, Pwhite(2.0,2.5),
					\rel, Pwhite(6.5,10.0),
					\ldb, 6,
					\amp, 0.7,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.875);
		});

		melodies.add(\ReSecondMelody -> {

		});

		melodies.add(\StopSecondMelody -> {
			Pdef(\melodies2).stop;
		});

		melodies.add(\ThirdMelody -> {
			Pdef(
				\melodies3,
				Pbind(
					\instrument, \bpfbuf,
					\dur, Pxrand( [Pseq([1/8], inf), Pseq([1/12], inf)],inf),
					\stretch, 1.875, // 128bpm -> 128/60bps -> 60/128spb -> 60/128*4spc = 1.875
					\buf, Pseq(
						[
							~buff[\doubleHarmonicScale_Shorts][1],
							~buff[\doubleHarmonicScale_Shorts][2],
							~buff[\doubleHarmonicScale_Shorts][3],
							~buff[\doubleHarmonicScale_Shorts][4],
							~buff[\doubleHarmonicScale_Shorts][5],
							~buff[\doubleHarmonicScale_Shorts][6],
							~buff[\doubleHarmonicScale_Shorts][7],
							~buff[\doubleHarmonicScale_Shorts][8],
							~buff[\doubleHarmonicScale_Shorts][9],
							~buff[\doubleHarmonicScale_Shorts][10],
							~buff[\doubleHarmonicScale_Shorts][11],
							~buff[\doubleHarmonicScale_Shorts][12],
						], 1
					),
					\amp, Pseq([3.8, 5.0], inf),
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.875);
		});

		melodies.add(\ReThirdMelody -> {

		});

		melodies.add(\StopThirdMelody -> {
			Pdef(\melodies3).stop;
		});

		oneShots.add(\FirstOS -> {
			Pdef(
				\desertOS,
				Pbind(
					\instrument, \bpfbuf,
					\buf, Pseq(
						[
							Prand(~buff[\desertOS], 1),
						], 1
					),
					\amp, 15.0,
					\rel, 20.0,
					\group, ~mainGrp,
					\out, ~bus[\reverb],
				);
			).play(quant:1.875);
		});

		oneShots.add(\SecondOS -> {
			15.do{
				Synth(
					\bpfbuf,
					[
						\atk, rrand(0.1,2.0),
						\sus, rrand(2.5,6.0),
						\rel, exprand(1.0,5.0),
						\c1, exprand(1,8),
						\c2, exprand(-8,-1),
						\buf, ~buff[\shakers][13].bufnum,
						\rate, exprand(0.3,1.2),
						\freq, (Scale.major.degrees.choose+64 + [-12,0,12,24].choose).midicps,
						\rq, exprand(0.002,0.02),
						\bpfmix, 1,
						\amp, exprand(2.2,4.5),
						\pan, rrand(-0.9,0.9),
						\spos, rrand(0,100000),
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