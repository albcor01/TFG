(
a = MusicMaker.new;
OSCdef.new(\Start,{ arg msg;
	a.init(msg[1]);
},'/Start', nil, 57120);
)