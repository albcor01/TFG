// SUPERCOLLIDER PIECE
// ---------------------------------------------------
// ---------------------------------------------------

(
// SERVER CONFIGURATION
// ---------------------------------------------------
// ---------------------------------------------------
s = Server.local;
s.options.numOutputBusChannels_(2);
s.options.sampleRate_(44000);
s.options.memSize_(2.pow(20));
s.newBusAllocators;
ServerBoot.removeAll;
ServerTree.removeAll;
ServerQuit.removeAll;

// GLOBAL VARIABLES INITIALIZATION
// ---------------------------------------------------
// ---------------------------------------------------
~out = 0;
~path = PathName(thisProcess.nowExecutingPath).parentPath++"buffers/";

// FUNCTIONS DEFINITIONS
// ---------------------------------------------------
// ---------------------------------------------------

// BUFFERS
// ---------------------------------------------------
~makeBuffers = {
	b = Dictionary.new;
	PathName(~path).entries.do{
		arg subfolder;
		b.add(
			subfolder.folderName.asSymbol ->
			Array.fill(
				subfolder.entries.size,
				{
					arg i;
					Buffer.read(s, subfolder.entries[i].fullPath);
				}
			)
		);
	};
};
// b[\folder][index].play;

// BUSSES
// ---------------------------------------------------
~makeBusses = {
	~bus = Dictionary.new;
	~bus.add(\reverb -> Bus.audio(s,2));
};

// NODES
// ---------------------------------------------------
~makeNodes = {
	s.bind({
		~mainGrp = Group.new;
		~reverbGrp = Group.after(~mainGrp);
		~reverbSynth = Synth.new(
			\reverb,
			[
				\amp, 1,
				\predelay, 0.1,
				\revtime, 1.8,
				\lpf, 4500,
				\mix, 0.75,
				\in, ~bus[\reverb],
				\out, ~out,
			],
			~reverbGrp
		);
	});
};

// EVENTS
// ---------------------------------------------------
~makeEvents = {
	e = Dictionary.new;
	e.add(\event1 -> {
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
	e.add(\event2 -> {
		~drone.stop;
	});

	e.add(\event3 -> {
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
	e.add(\event4 -> {
		~bubbles.stop;
	});

	e.add(\event5 -> {
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
	e.add(\event6 -> {
		~simpleMarimba.stop;
	});

	e.add(\event7 -> {
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

	e.add(\event8 -> {
		~marimba.stop;
		~chords.stop;
	});

	e.add(\oneshot1 -> {
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

	e.add(\oneshot2 -> {

	});

	e.add(\oneshot3 -> {

	});
};

// CLEANING
// ---------------------------------------------------
~cleanup = {
	s.newBusAllocators;
	ServerBoot.removeAll;
	ServerTree.removeAll;
	ServerQuit.removeAll;
};

// FUNCTIONS REGISTRATIONS W/SERVER
// ---------------------------------------------------
// ---------------------------------------------------
ServerBoot.add(~makeBuffers);
ServerBoot.add(~makeBusses);
ServerQuit.add(~cleanup);

// BOOT SERVER
// ---------------------------------------------------
// ---------------------------------------------------
s.waitForBoot({

	s.sync;

	~unityClient = NetAddr.new("127.0.0.1",7771);
	NetAddr.localAddr;
	b = NetAddr.new("127.0.0.1", 7771);

	s.sync;

	// SAW SYNTH
	// ---------------------------------------------------
	SynthDef(\bpfsaw, {
		arg atk=2, sus=0, rel=3, c1=1, c2=(-1),
		freq=500, detune=0.2, pan=0, cfhzmin=0.1, cfhzmax=0.3,
		cfmin=500, cfmax=2000, rqmin=0.1, rqmax=0.2,
		lsf=200, ldb=0, amp=1, out=0;
		var sig, env;
		env = EnvGen.kr(Env([0,1,1,0],[atk,sus,rel],[c1,0,c2]),doneAction:2);
		sig = Saw.ar(freq * {LFNoise1.kr(0.5,detune).midiratio}!2);
		sig = BPF.ar(
			sig,
			{LFNoise1.kr(
				LFNoise1.kr(4).exprange(cfhzmin,cfhzmax)
			).exprange(cfmin,cfmax)}!2,
			{LFNoise1.kr(0.1).exprange(rqmin,rqmax)}!2
		);
		sig = BLowShelf.ar(sig, lsf, 0.5, ldb);
		sig = Balance2.ar(sig[0], sig[1], pan);
		sig = sig * env * amp;
		Out.ar(out, sig);
	}).add;

	// SAMPLES
	// ---------------------------------------------------
	SynthDef(\bpfbuf, {
		arg atk=0, sus=0, rel=3, c1=1, c2=(-1),
		buf=0, rate=1, spos=0, freq=440, rq=1, bpfmix=0,
		pan=0, amp=1, out=0;
		var sig, env;
		env = EnvGen.kr(Env([0,1,1,0],[atk,sus,rel],[c1,0,c2]),doneAction:2);
		sig = PlayBuf.ar(1, buf, rate*BufRateScale.ir(buf),startPos:spos);
		sig = XFade2.ar(sig, BPF.ar(sig, freq, rq, 1/rq.sqrt), bpfmix*2-1);
		sig = sig * env;
		sig = Pan2.ar(sig, pan, amp);
		Out.ar(out, sig);
	}).add;

	// REVERB
	// ---------------------------------------------------
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
		Out.ar(out, sig);
	}).add;

	s.sync;

	ServerTree.add(~makeNodes);
	ServerTree.add(~makeEvents);
	s.freeAll;

	s.sync;

	"done".postln;
});
)

e[\event1].value;
e[\event2].value;
e[\event3].value;
e[\event4].value;
e[\event5].value;
e[\event6].value;
e[\event7].value;
e[\event8].value;
e[\oneshot1].value;

(
OSCdef.new(
	\dronPlay,
	{
		e[\event1].value;
	},
	'/dronPlay', nil, 57120
);

OSCdef.new(
	\dronStop,
	{
		e[\event2].value;
	},
	'/dronStop', nil, 57120
);

OSCdef.new(
	\bubblesPlay,
	{
		e[\event3].value;
	},
	'/bubblesPlay', nil, 57120
);

OSCdef.new(
	\bubblesStop,
	{
		e[\event4].value;
	},
	'/bubblesStop', nil, 57120
);

OSCdef.new(
	\musicPlay,
	{
		e[\event7].value;
	},
	'/musicPlay', nil, 57120
);

OSCdef.new(
	\musicStop,
	{
		e[\event8].value;
	},
	'/musicStop', nil, 57120
);

OSCdef.new(
	\sfxPlay,
	{
		e[\oneshot1].value;
	},
	'/sfxPlay', nil, 57120
);
)

s.quit;

















