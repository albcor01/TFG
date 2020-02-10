//--INIT--
(
s.boot;
Environment.clear;

~unityClient = NetAddr.new("127.0.0.1",7771);
NetAddr.localAddr;
b = NetAddr.new("127.0.0.1", 7771);
)

//--EXAMPLES--

//BASIC OSCILATOR
(
OSCdef.new(
	\event0,
	{
		z = {
			var freq = 440, amp = 1, sig;
			sig = SinOsc.ar(freq) * amp;
		}.play;
	},
	'/event0', nil, 57120
);
)

//RANDOM VARIABLES
(
OSCdef.new(
	\event1,
	{
		y = {
			arg noiseHz = 8;
			var freq, amp, sig;
			freq = LFNoise0.kr(noiseHz).exprange(200, 1000);
			amp = LFNoise1.kr(12).exprange(0.7, 1);
			sig = SinOsc.ar(freq) * amp;
		}.play;
	},
	'/event1', nil, 57120
);
)

//CHANGE NOISE FREQ
(
OSCdef.new(
	\event2,
	{
		y.set(\noiseHz, exprand(4, 64));
	},
	'/event2', nil, 57120
);
)

//SYNTHDEF
(
OSCdef.new(
	\event3,
	{
		SynthDef.new(\sineTest, {
			arg noiseHz = 8;
			var freq, amp, sig;
			freq = LFNoise0.kr(noiseHz).exprange(200, 1000);
			amp = LFNoise1.kr(12).exprange(0.7, 1);
			sig = SinOsc.ar(freq) * amp;
			Out.ar(0, sig); //0 = left channel
		}).add;

		x = Synth.new(\sineTest);
		//x = Synth.new(\sineTest, [\noiseHz, 32]);
		//x.set(\noiseHz, 12);
	},
	'/event3', nil, 57120
);
)

//SYNTHDEF
(
OSCdef.new(
	\event4,
	{
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

		w = Synth.new(\pulseTest);
		//w = Synth.new(\pulseTest, [\ampHz, 3.3, \fund, 48, \maxPartial, 4, \width, 0.15]);
		//w.set(\width, 0.25);
		//w.set(\fund, 30);
		//w.set(\maxPartial, 20);
		//w.set(\ampHz, 2);
	},
	'/event4', nil, 57120
);
)

//STOPS EVERY SOUND
(
OSCdef.new(
	\eventStop,
	{
		z.free;
		y.free;
		x.free;
		w.free;
	},
	'/eventStop', nil, 57120
);
)