
DesertPackage {

	//Variables de la clase
	var <e, params;

		init{ arg parameters;

		e = Dictionary.new;
		params = parameters;

		e.add(\DesertPercs -> {
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

		e.add(\ReDesertPercs -> {
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

		e.add(\StopDesertPercs -> {
			Pdef(\rhythm).stop;
		});

		e.add(\DesertShakers -> {
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

		e.add(\StopDesertShakers -> {
			~shakerSustain.stop;
		});

		e.add(\DesertChord -> {
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

		e.add(\StopDesertChord -> {
			Pdef(\chord).stop;
		});

		e.add(\DesertMelody -> {
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

		e.add(\ReDesertMelody -> {
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

		e.add(\StopDesertMelody -> {
			Pdef(\melodies).stop;
		});

		e.add(\DesertMelody2 -> {
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

		e.add(\StopDesertMelody2 -> {
			Pdef(\melodies2).stop;
		});

		e.add(\DesertMelody3 -> {
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

		e.add(\StopDesertMelody3 -> {
			Pdef(\melodies3).stop;
		});

		e.add(\DesertMelodyOS -> {
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

		e.add(\StopDesertMelodyOS -> {
			Pdef(\desertOS).stop;
		});

		e.add(\DesertShakersOS -> {
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

		//Mensajes
		//...
		//OSCdef.new(
		//	\MsgName,
		//	{
		//		arg msg;
		//		if(msg[1] == 1)
		//		{
		//			e[\EventName].value;
		//		}
		//		{
		//			e[\StopEventName].value;
		//		}
		//	},
		//	'/MsgName', nil, 57120
		//);
	}

	test{
		e[\DesertPercs].value;
	}
	test2{
		e[\ReDesertPercs].value;
	}
	test3{
		e[\StopDesertPercs].value;
	}

	test4{
		e[\DesertShakers].value;
	}
	test5{
		e[\StopDesertShakers].value;
	}

	test6{
		e[\DesertChord].value;
	}
	test7{
		e[\StopDesertChord].value;
	}

	test8{
		e[\DesertMelody].value;
	}
	test9{
		e[\ReDesertMelody].value;
	}
	test10{
		e[\StopDesertMelody].value;
	}

	test11{
		e[\DesertMelody2].value;
	}
	test12{
		e[\StopDesertMelody2].value;
	}

	test13{
		e[\DesertMelody3].value;
	}
	test14{
		e[\StopDesertMelody3].value;
	}

	test15{
		e[\DesertMelodyOS].value;
	}
	test16{
		e[\StopDesertMelodyOS].value;
	}

	test17{
		e[\DesertShakersOS].value;
	}
}