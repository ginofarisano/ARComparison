PostTest <- read.csv("~/Documents/R finale/PostTest.csv", header=FALSE, sep=";")

par(mfrow=c(1,3))

manual<-Pretest[1:6,]
HHD<-Pretest[7:12,]
HMD<-Pretest[13:18,]

meanFirstColumnManual<-mean(manual[,1])
meanFirstColumnHHD<-mean(HHD[,1])
meanFirstColumnHMD<-mean(HMD[,1])

allMeanFirstColumn<-c(meanFirstColumnManual,meanFirstColumnHHD,meanFirstColumnHMD)

bp<-barplot(allMeanFirstColumn,ylab = "Rating",names.arg = c("Manuale tradizionale","HHD AR","HMD AR"),col = colors,ylim=c(0,max(allMeanSecondColumn)+1),main = "È stato complicato completare i task proposti con il sistema utilizzato?")

meanSecondColumnManual<-mean(manual[,2])
meanSecondColumnHHD<-mean(HHD[,2])
meanSecondColumnHMD<-mean(HMD[,2])

allMeanSecondColumn<-c(meanSecondColumnManual,meanSecondColumnHHD,meanSecondColumnHMD)

bp<-barplot(allMeanSecondColumn,ylab = "Rating",names.arg = c("Manuale tradizionale","HHD AR","HMD AR"),col = colors,ylim=c(0,max(allMeanFirstColumn)+1),main = "Le istruzioni presentate dal sistema sono risultate chiare?")

meanThirdColumnManual<-mean(manual[,3])
meanThirdColumnHHD<-mean(HHD[,3])
meanThirdColumnHMD<-mean(HMD[,3])

allMeanThirdColumn<-c(meanThirdColumnManual,meanThirdColumnHHD,meanThirdColumnHMD)

bp<-barplot(allMeanThirdColumn,ylab = "Rating",names.arg = c("Manuale tradizionale","HHD AR","HMD AR"),col = colors,ylim=c(0,max(allMeanThirdColumn)+1),main = "Una procedura di manutenzione è più semplice se utilizza l'AR?")

