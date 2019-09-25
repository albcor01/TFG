//aparte de ctrl+b podemos usar esta linea para iniciar el server o pararlo
s.boot;
s.quit;

//ESTRUCTURAS BÁSICAS DE CONTROL
(
var i = rrand(0,100);
if(i > 50, {"mayor".postln;}, {"menor".postln;});
)

(
for(0,50,{arg i; i.postln});
)

(
var i = 0;
while({i < 30}, {i=i+1; i.postln});
)

5.do({"hola".postln});

(
x=rrand(0,2);
switch(x, 0,{"primera opcion".postln}, 1,{"segunda opcion".postln}, 2,{"tercera opcion".postln});
)

(
var rit_aleat = Array.fill(6, {arg i; i = rrand(0.1, 1)});

SynthDef(\prueba, {arg freq, dur;
	var signal, env;
	env = EnvGen.kr(Env([0,1,0], [dur/2, dur/2]), doneAction: 2);
	signal= SinOsc.ar(freq);
	Out.ar(0, Pan2.ar(signal,0,1))
}).add;

SynthDef(\kalimba, {
    |out = 0, freq = 440, amp = 0.1, mix = 0.1|
    var snd;
    // Basic tone is a SinOsc
    snd = SinOsc.ar(freq) * EnvGen.ar(Env.perc(0.005, Rand(2.5, 3.5), 1, -8), doneAction: 2);
    // The "clicking" sounds are modeled with a bank of resonators excited by enveloped pink noise
    snd = (snd * (1 - mix)) + (DynKlank.ar(`[
        // the resonant frequencies are randomized a little to add variation
        // there are two high resonant freqs and one quiet "bass" freq to give it some depth
        [240*ExpRand(0.9, 1.1), 2020*ExpRand(0.9, 1.1), 3151*ExpRand(0.9, 1.1)],
        [-7, 0, 3].dbamp,
        [0.8, 0.05, 0.07]
    ], PinkNoise.ar * EnvGen.ar(Env.perc(0.001, 0.01))) * mix);
    Out.ar(out, Pan2.ar(snd, 0, amp));
}).add;



Pbind(*[
	\instrument, \kalimba,
	\freq, Pseq([60,61,62,63,64,65].midicps),
	\dur, Pseq(rit_aleat)
]).play
)
