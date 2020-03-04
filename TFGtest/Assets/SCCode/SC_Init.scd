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
// CREAMOS LAS DISTINTAS FUNCIONES QUE QUEREMOS QUE SE LLAMEN AL HACER BOOT DEL SERVIDOR
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
ServerBoot.add(~synths);
ServerBoot.add(~effects);
ServerQuit.add(~cleanup);

s.waitForBoot({

	s.sync;
	~unityClient = NetAddr.new("127.0.0.1",7771);
	NetAddr.localAddr;
	b = NetAddr.new("127.0.0.1", 7771);
	s.sync;



	s.sync;
	ServerTree.add(~makeNodes);
	ServerBoot.add(~makeEvents);
	s.freeAll;
	s.sync;

	"done".postln;
});
)



