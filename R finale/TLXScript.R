require(PMCMR)

par(mfrow=c(2,3))


TLX <- read.csv("~/Documents/R finale/TLX.csv", sep=";")

TLXManual<-TLX[,1:6]
TLXHHD<-TLX[,7:12]
TLXHMD<-TLX[,13:18]

TLXManualRating<-TLXManual[1:6,]
TLXHHDRating<-TLXHHD[1:6,]
TLXHMDRating<-TLXHMD[1:6,]

TLXManualGlobalRating<-TLXManual[7,]
TLXHHDGlobalRating<-TLXHHD[7,]
TLXHMDGlobalRating<-TLXHMD[7,]

TLXManualWeight<-TLXManual[8:13,]
TLXHHDWeight<-TLXHHD[8:13,]
TLXHMDWeight<-TLXHMD[8:13,]

TLXManualMeanRating<-rowMeans(TLXManualRating)
TLXHHDMeanRating<-rowMeans(TLXHHDRating)
TLXHMDMeanRating<-rowMeans(TLXHMDRating)

TLXManualMeanWeight<-rowMeans(TLXManualWeight)
TLXHHDMeanWeight<-rowMeans(TLXHHDWeight)
TLXHMDMeanWeight<-rowMeans(TLXHMDWeight)

labelBp<-c("Mental Demand","Physical Demand","Temporal Demand","Performance","Effort","Frustration")

colors <- rainbow(3) 

for(i in 1:6){
  
  metric<-c(TLXManualMeanRating[i],TLXHHDMeanRating[i],TLXHMDMeanRating[i])
  
  bp<-barplot(metric,ylab = "Rating",names.arg = c("Manuale tradizionale","HHD AR","HMD AR"),main = labelBp[i],col = colors,ylim=c(0,max(metric)+5))
  text(x=bp,c(TLXManualMeanRating[i]+2,TLXHHDMeanRating[i]+2,TLXHMDMeanRating[i]+2),labels=c(paste("W:",round(TLXManualMeanWeight[i],2)),paste("W:",round(TLXHHDMeanWeight[i],2)),paste("W:",round(TLXHMDMeanWeight[i],2))))
}

#ora faccio Anova solo per il rating globale

myClass<-c(rep("A",6),rep("B",6),rep("C",6))
TLXGlobalRating<-data.frame(c(TLXManualGlobalRating,TLXHHDGlobalRating,TLXHMDGlobalRating))
TLXGlobalRating<-data.frame(t(TLXGlobalRating),myClass)

summary(t(TLXManualGlobalRating))
summary(t(TLXHHDGlobalRating))
summary(t(TLXHMDGlobalRating))

par(mfrow=c(1,1))


boxplot(TLXGlobalRating[1:18,1] ~ myClass[1:18], data=TLXGlobalRating,main="Boxplot rating medi NASA-TLX",names=c("Manuale tradizionale","HHD AR","HMD AR"),col = colors,ylab="Rating medio")


#ipotesi normalità
shapiro.test(as.numeric(TLXManualGlobalRating))

shapiro.test(as.numeric(TLXHHDGlobalRating))


shapiro.test(as.numeric(TLXHMDGlobalRating))

bartlett.test(TLXGlobalRating[1:18,1] ~ myClass,data=TLXGlobalRating)

results = aov(TLXGlobalRating[1:18,1] ~ myClass, data=TLXGlobalRating)

summary(results)
posthoc2 = glht(results, linfct = mcp(myClass = "Tukey"))
plot(posthoc2)

pairwise.t.test(TLXGlobalRating[1:18,1], myClass, p.adjust="bonferroni", alternative = c("two.sided","less", "greater"))

kruskal.test(TLXGlobalRating[1:18,1] ~ myClass, data=TLXGlobalRating)

posthoc.kruskal.nemenyi.test(TLXGlobalRating[1:18,1] ~ myClass, data=TLXGlobalRating, dist="Tukey")

(out <- posthoc.kruskal.nemenyi.test(TLXGlobalRating[1:18,1] ~ factor(myClass), dist="Chisquare"))


