miofinale <- read.table("~/Documents/TESI FINALE/AR-EXPERIMENTATION/AR/result/miofinale.csv", quote="\"", comment.char="")
altrofinale <- read.table("~/Documents/TESI FINALE/AR-EXPERIMENTATION/AR/result/altrofinale.csv", quote="\"", comment.char="")


primografico<-miofinale[,1]

secondografico<-altrofinale[,1]

par(mfrow=c(2,1))

plot(1:21986,pch="|",primografico, col=primografico,cex=0.4,xlab="Frame number", ylab="Tracked side",yaxt = "n",ylim=c(0.5,6),main = "More side scan algorithm")
axis(2, at=1:6, labels=c("Nothing","Top Side","CMOS Side","Ram Side","CPU Side","Input Side"),cex.axis=0.55)
  
plot(1:21986,pch="|",secondografico, col=secondografico,cex=0.4,xlab="Frame number", ylab="Tracked side",yaxt = "n",ylim=c(0.5,2.5),main="One side scan algorithm")
axis(2, at=1:2, labels=c("Nothing","Top Side"),cex.axis=0.8) 
  


