
// DEBERIAMOS LLAMAR AL METODO OSC "INIT" NO?

(
a = MusicMaker.new;
OSCdef.new(\Start,{
	arg msg;
	a.init(msg[1]);
},'/Start', nil, 57120);
)

//////

a = MusicMaker.new;
a.init(\Fantasy);
a.start;
a.start2;
a.start3;
a.start4;
a.start5;
a.start6;
a.start7;
a.start8;
a.start9;
a.start10;
a.stop;