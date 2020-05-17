
// METODO QUE INICIARÁ EL MUSIC MAKER DESDE UNITY

(
a = MusicMaker.new;
OSCdef.new(\Init,{
	arg msg;
	a.init(msg[1]);
},'/Init', nil, 57120);
)

// DEBUG DE LOS PAQUETES DESDE SUPERCOLLIDER

a = MusicMaker.new;
a.init(\Fantasy);

a.d_playRhythm(2);
a.d_stopRhythm(0);

a.d_playHarmony(0);
a.d_stopHarmony(0);

a.d_playMelody(0);
a.d_stopMelody(0);

a.d_playOS(2);