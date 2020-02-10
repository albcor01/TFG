// SUPERCOLLIDER CODE

// INIT AND QUIT THE SERVER
// ---------------------------------------------------
s.boot;
s.quit;

// PLOT TREE
// ---------------------------------------------------
s.plotTree;

// METER
// ---------------------------------------------------
s.meter;

// FREE MEM
s.freeAll;

// SCALE
Scale.major;
Scale.minor;
(Scale.minor.degrees+60).midicps; //60 == middle C

// INITIALIZE WITH UNITY
// ---------------------------------------------------
(
s.boot;
Environment.clear;

~unityClient = NetAddr.new("127.0.0.1",7771);

NetAddr.localAddr;
~address = NetAddr.new("127.0.0.1", 7771);
)

// BASIC OSCILATOR
// ---------------------------------------------------
(
~basic_osc = {
	var freq = 440, amp = 1, sig;
	sig = SinOsc.ar(freq) * amp;
}.play;
)

~basic_osc.free;

// OSCILATOR RANDOM FREQ AND AMP
// ---------------------------------------------------
(
~rand_osc = {
	arg noiseHz = 8;
	var freq, amp, sig;
	freq = LFNoise0.kr(noiseHz).exprange(200, 1000);
	amp = LFNoise1.kr(12).exprange(0.7, 1);
	sig = SinOsc.ar(freq) * amp;
}.play;
)

// OSCILATOR ARGS CHANGES
// ---------------------------------------------------
~rand_osc.set(\noiseHz, 4);
~rand_osc.set(\noiseHz, exprand(4, 64));

~rand_osc.free;

// SYNTH DEF
// ---------------------------------------------------
(
SynthDef.new(\sineTest, {
	arg noiseHz = 8;
	var freq, amp, sig;
	freq = LFNoise0.kr(noiseHz).exprange(200, 1000);
	amp = LFNoise1.kr(12).exprange(0.7, 1);
	sig = SinOsc.ar(freq) * amp;
	Out.ar(0, sig); //0 = left channel
}).add;
)

~rand_osc2 = Synth.new(\sineTest);
~rand_osc2 = Synth.new(\sineTest, [\noiseHz, 32]);
~rand_osc2.set(\noiseHz, 12);

~rand_osc2.free;

(
SynthDef.new(\pulseTest, {
	arg ampHz = 4, fund = 40, maxPartial = 4, width = 0.5;
	var amp1, amp2, freq1, freq2, sig1, sig2;
	amp1 = LFPulse.kr(ampHz, 0, 0.12) * 0.75;
	amp2 = LFPulse.kr(ampHz, 0.5, 0.12) * 0.75;
	freq1 = LFNoise0.kr(4).exprange(fund, fund * maxPartial).round(fund);
	freq2 = LFNoise0.kr(4).exprange(fund, fund * maxPartial).round(fund);
	freq1 = freq1 * LFPulse.kr(8, add:1);
	freq2 = freq2 * LFPulse.kr(6, add:1);
	sig1 = Pulse.ar(freq1, width, amp1);
	sig2 = Pulse.ar(freq2, width, amp2);
	sig1 = FreeVerb.ar(sig1, 0.7, 0.8, 0.25);
	sig2 = FreeVerb.ar(sig2, 0.7, 0.8, 0.25);
	Out.ar(0, sig1);
	Out.ar(1, sig2);
}).add;
)

~rand_osc3 = Synth.new(\pulseTest);
~rand_osc3 = Synth.new(\pulseTest, [\ampHz, 3.3, \fund, 48, \maxPartial, 4, \width, 0.15]);
~rand_osc3.set(\width, 0.25);
~rand_osc3.set(\fund, 30);
~rand_osc3.set(\maxPartial, 20);
~rand_osc3.set(\ampHz, 2);

~rand_osc3.free;

// CHORD GENERATOR
// ---------------------------------------------------
(
(3..6).choose.do{
	//Synth(
	//	\bpfsaw,
	//	[
	//		\freq, (Scale.minor.degrees+60).midicps.choose,
	//	]
	//);
};
)

// ENVELOPES
// ---------------------------------------------------
(
~pulse_osc = {
	var sig, env;
	env = Line.kr(1, 0, 1, doneAction:2); //dA = 2, cuando termine se libera
	sig = Pulse.ar(ExpRand(30, 500) * env);
}.play;
)

~pulse_osc.free;

(
~pulse_osc2 = {
	arg t_gate = 0;
	var sig, env;
	env = EnvGen.kr(Env.new(
		[0, 1, 0.2, 0],
		[0.5, 1, 2],
		[3, -3, 0]), t_gate, doneAction:2);
	sig = Pulse.ar(LFPulse.kr(8).range(600, 900)) * env;
}.play;
)

~pulse_osc2.set(\t_gate, 1);

~pulse_osc2.free;

// ITERATIONS
// ---------------------------------------------------
(
SynthDef.new(\iter, {
	arg freq = 40;
	var temp, sum, env;
	sum = 0;
	env = EnvGen.kr(
		Env.perc(0.01, 5, 1, -2),
		doneAction: 2
	);

	10.do{
		temp = VarSaw.ar(
			freq * {Rand(0.99, 1.02)}!2,
			{Rand(0, 1)}!2,
			{ExpRand(0.005, 0.05)}!2
		);
		sum = sum + temp;
	};
	sum = sum * 0.05 * env;
	Out.ar(0, sum);
}).add;
)

Synth.new(\iter);
Synth.new(\iter, [\freq, 440]);
Synth.new(\iter, [\freq, 880]);
Synth.new(\iter, [\freq, 66.midicps]); // EN MIDI

(
[53, 59, 63, 68].do{
	arg midinote;
	Synth.new(\iter, [\freq, midinote.midicps]);
}
)