//--INIT--
(
s.boot;
Environment.clear;

~unityClient = NetAddr.new("127.0.0.1",7771);
NetAddr.localAddr;
b = NetAddr.new("127.0.0.1", 7771);
)

//--EVENTS--
(
//BASIC OSCILATOR
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

//RANDOM VARIABLES
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

//CHANGE NOISE FREQ
OSCdef.new(
	\event2,
	{
		y.set(\noiseHz, exprand(4, 64));
	},
	'/event2', nil, 57120
);

//STOPS EVERY SOUND
OSCdef.new(
	\eventStop,
	{
		z.free;
		y.free;
	},
	'/eventStop', nil, 57120
);
)