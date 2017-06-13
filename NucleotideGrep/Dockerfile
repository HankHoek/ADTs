FROM	mono:latest
RUN	apt-get update && apt-get install -y vim
WORKDIR	src
ADD	. .
RUN	msbuild NucleotideGrep.csproj