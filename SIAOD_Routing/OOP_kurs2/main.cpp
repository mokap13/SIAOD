#include <time.h>
#include <iostream>
#include "CompoziteDetail.h"
#include "Detail.h"
#include "Bearing.h"
#include "MaleScrew.h"
#include "FemaleScrew.h"
#include <conio.h>

using namespace std;

void main() {	
	MaleScrew maleScrew;
	maleScrew.setWeight(8.8);
	FemaleScrew femaleScrew;
	femaleScrew.setWeight(12.5);
	Bearing bearing;
	bearing.setWeight(4.6);


	Detail::Sptr detailA(new CompoziteDetail("ANode"));
	detailA->add(Detail::Sptr(&maleScrew));
	detailA->add(Detail::Sptr(&maleScrew));
	detailA->add(Detail::Sptr(&femaleScrew));
	detailA->add(Detail::Sptr(&femaleScrew));

	Detail::Sptr detailB(new CompoziteDetail("BNode"));
	detailB->add(Detail::Sptr(&bearing));
	detailB->add(Detail::Sptr(&bearing));
	detailB->add(Detail::Sptr(&bearing));

	Detail::Sptr detailC(new CompoziteDetail("CNode"));
	detailC->add(Detail::Sptr(&maleScrew));
	detailC->add(Detail::Sptr(&maleScrew));
	detailC->add(detailB);

	Detail::Sptr mainDetail(new CompoziteDetail("MainNode"));
	mainDetail->add(detailB);
	mainDetail->add(detailA);
	mainDetail->add(detailC);

	mainDetail->display();
	_getch();
}