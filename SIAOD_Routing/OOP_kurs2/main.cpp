#include <time.h>
#include <iostream>
#include "CompoziteDetail.h"
#include "Detail.h"
#include "Bearing.h"
#include "MaleScrew.h"
#include "FemaleScrew.h"

using namespace std;

void main() {
	Detail::Sptr maleScrew_1(new MaleScrew());
	Detail::Sptr femaleScrew_1(new FemaleScrew());

	Detail::Sptr detailA(new CompoziteDetail("ANode"));
	detailA->add(maleScrew_1);
	detailA->add(maleScrew_1);
	detailA->add(femaleScrew_1);
	detailA->add(femaleScrew_1);

	Detail::Sptr detailB(new CompoziteDetail("BNode"));
	Detail::Sptr detailC(new CompoziteDetail("CNode"));

	

	Detail::Sptr mainDetail(new CompoziteDetail("MainNode"));
}