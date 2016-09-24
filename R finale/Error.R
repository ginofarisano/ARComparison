Error <- read.csv("~/Documents/R finale/Error.csv", sep=";")

par(mfrow=c(1,2))


Error<-abs(Error-1)

colors <- rainbow(3) 
linetype <- c(1:3) 
plotchar <- seq(18,18+3,1)

colors <- rainbow(3) 
linetype <- c(1:3) 
plotchar <- seq(18,18+3,1)

ErrorManual<-Error[1:6,]
ErrorHHD<-Error[7:12,]
ErrorHMD<-Error[13:18,]

ErrorManualMeanForTask<-colMeans(ErrorManual)
ErrorHHDMeanForTask<-colMeans(ErrorHHD)
ErrorHMDMeanForTask<-colMeans(ErrorHMD)

maxMean<-max(ErrorManualMeanForTask,ErrorHHDMeanForTask,ErrorHMDMeanForTask)

# set up the plot
plot(c(1,20),c(0,maxMean),type="n",xlab="Task number",ylab="Errori medi",main = "Errori medi task per gruppo",xaxt = "n")
axis(1, at=1:20, labels=3:22,cex.axis=0.8)

#plot line
lines(1:20, ErrorManualMeanForTask, type="b", lwd=1.5,
      lty=linetype[1], col=colors[1], pch=plotchar[1]) 

lines(1:20, ErrorHHDMeanForTask, type="b", lwd=1.5,
      lty=linetype[2], col=colors[2], pch=plotchar[2]) 

lines(1:20, ErrorHMDMeanForTask, type="b", lwd=1.5,
      lty=linetype[3], col=colors[3], pch=plotchar[3]) 

legend(12.5, 0.42, c("Manuale tradizionale","HHD AR","HMD AR"), cex=0.6, col=colors,
       pch=plotchar, lty=linetype)

summary(ErrorManualMeanForTask)
summary(ErrorHHDMeanForTask)
summary(ErrorHMDMeanForTask)

#anova faccio la media dei task per ogni esperimento
ErrorManualMeanForExperiment<-rowMeans(ErrorManual)
ErrorHHDMeanForExperiment<-rowMeans(ErrorHHD)
ErrorHMDMeanForExperiment<-rowMeans(ErrorHMD)

summary(ErrorManualMeanForExperiment)
summary(ErrorHHDMeanForExperiment)
summary(ErrorHMDMeanForExperiment)

ErrorMeanForExperiment<-NULL
#li concateno
ErrorMeanForExperiment<-data.frame(c(ErrorManualMeanForExperiment,ErrorHHDMeanForExperiment,ErrorHMDMeanForExperiment))

#assegno label ad ogni esperimento
myClass<-c(rep("A",6),rep("B",6),rep("C",6))

ErrorMeanForExperiment<-data.frame(ErrorMeanForExperiment,myClass)

#ipotesi normalità
shapiro.test(ErrorManualMeanForExperiment)

shapiro.test(ErrorHHDMeanForExperiment)

shapiro.test(ErrorHMDMeanForExperiment)

#ipotesi variaza

bartlett.test(ErrorMeanForExperiment[1:18,1] ~ myClass,data=ErrorMeanForExperiment)

boxplot(ErrorMeanForExperiment[1:18,1] ~ myClass[1:18], data=ErrorMeanForExperiment,main="Boxplot errori medi task",names=c("Manuale tradizionale","HHD AR","HMD AR"),col = colors,ylab="Errori medi")

results = aov(ErrorMeanForExperiment[1:18,1] ~ myClass, data=ErrorMeanForExperiment)

summary(results)

kruskal.test(ErrorMeanForExperiment[1:18,1] ~ myClass, data=ErrorMeanForExperiment) 

