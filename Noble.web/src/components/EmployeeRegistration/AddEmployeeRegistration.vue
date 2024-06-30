<template>

    <div class="row" v-if="isValid('CanAddEmployeeReg') || isValid('CanEditEmployeeReg') ">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-12">
                    <div class="page-title-box">
                        <div class="row">
                            <div class="col">
                                <h4 class="page-title" v-if="employee.id== '00000000-0000-0000-0000-000000000000'">{{ $t('AddEmployeeRegistration.AddEmployee') }}</h4>
                                <h4 class="page-title" v-else>{{ $t('AddEmployeeRegistration.UpdateEmployee') }}</h4>
                                <ol class="breadcrumb">
                                    <li class="breadcrumb-item"><a href="javascript:void(0);">{{ $t('AddEmployeeRegistration.Home') }}</a></li>
                                    <li class="breadcrumb-item active" v-if="employee.id== '00000000-0000-0000-0000-000000000000'">{{ $t('AddEmployeeRegistration.AddEmployee') }}</li>
                                    <li class="breadcrumb-item active" v-else>{{ $t('AddEmployeeRegistration.UpdateEmployee') }}</li>
                                </ol>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card">
                <div class="card-body">
                    <div class="row">
                        <div>
                            <ul class="nav nav-tabs" data-tabs="tabs">
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'PersonalInfo'}" v-on:click="makeActive('PersonalInfo')" id="v-pills-home-tab" data-toggle="pill" href="#v-pills-home" role="tab" aria-controls="v-pills-home" aria-selected="false">{{ $t('AddEmployeeRegistration.PersonalInformation') }}</a></li>
                                <li class="nav-item" v-if="isValid('CanViewRunPayroll') || isValid('CanAddRunPayroll') || isValid('CanDraftRunPayroll')"><a class="nav-link" v-bind:class="{active:active == 'Salary '}" v-on:click="makeActive('Salary')" id="v-pills-profile1" data-toggle="pill" href="#v-pills-profile1" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('AddEmployeeRegistration.Salary') }}</a></li>
                                <li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Emergency '}" v-on:click="makeActive('Emergency')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile2" role="tab" aria-controls="v-pills-profile2" aria-selected="false">{{ $t('AddEmployeeRegistration.Emergency') }}</a></li>
                                <li class="nav-item" v-if="IsPaksitanClient"><a class="nav-link" v-bind:class="{active:active == 'HomeCountry '}" v-on:click="makeActive('HomeCountry')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile3" role="tab" aria-controls="v-pills-profile3" aria-selected="false">{{ $t('AddEmployeeRegistration.HomeCountryContactInfo') }}</a></li>
                                <li class="nav-item" v-if="IsPaksitanClient"><a class="nav-link" v-bind:class="{active:active == 'LegalIdentity '}" v-on:click="makeActive('LegalIdentity')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile4" role="tab" aria-controls="v-pills-profile4" aria-selected="false">{{ $t('AddEmployeeRegistration.LegalIdentity') }}</a></li>
                                <!--<li class="nav-item"><a class="nav-link" v-bind:class="{active:active == 'Attachment '}" v-on:click="makeActive('Attachment')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false">{{ $t('AddEmployeeRegistration.EmployeeAttachment') }}</a></li>-->
                                <li class="nav-item" v-if="IsPaksitanClient"><a class="nav-link" v-bind:class="{active:active == 'Attachment '}" v-on:click="makeActive('TemporaryCashRequest')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile5" role="tab" aria-controls="v-pills-profile5" aria-selected="false">{{ $t('AddEmployeeRegistration.TemporaryCashRequest') }}</a></li>
                                <li class="nav-item" ><a class="nav-link" v-bind:class="{active:active == 'Bank '}" v-on:click="makeActive('Bank')" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile5" role="tab" aria-controls="v-pills-profile5" aria-selected="false">Bank</a></li>

                            </ul>
                        </div>
                        <div class="tab-content" id="nav-tabContent">

                            <div v-if="active == 'PersonalInfo'">
                                <div class="card-body ">

                                    <div class="row">
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.EmployeeType') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.employeeType.$error}">
                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" @update:modelValue="ChangeCode" v-model="v$.employee.employeeType.$model" :options="['Permanent','Probation','Internee','Temporary']" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :show-labels="false" :placeholder="$t('AddEmployeeRegistration.SelectEmployeeType')">
                                                </multiselect>
                                                <multiselect v-else v-model="v$.employee.employeeType.$model" :options="['مدير', 'مقاول', 'مشرف','مشرف','تعب']" :show-labels="false" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :placeholder="$t('AddEmployeeRegistration.SelectEmployeeType')">
                                                </multiselect>
                                                <span v-if="v$.employee.employeeType.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.EmployeeCode') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.code.$error}">
                                                <input readonly  class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.code.$model" />
                                                <span v-if="v$.employee.code.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.RegistrationDate') }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.employee.registrationDate.$error}">
                                                <datepicker v-model="v$.employee.registrationDate.$model" :key="daterander"></datepicker>

                                            </div>
                                        </div>
                                        <div v-if="english=='true'" class="col-sm-4  mb-2">
                                            <label>{{$englishLanguage($t('AddEmployeeRegistration.EmployeeName(English)'))  }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.employee.englishName.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.englishName.$model" />
                                                <span v-if="v$.employee.englishName.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.englishName.required">{{ $t('AddEmployeeRegistration.EngValidation') }}</span>
                                                    <span v-if="!v$.employee.englishName.maxLength">{{ $t('AddEmployeeRegistration.EngMax') }}</span>

                                                </span>
                                            </div>
                                        </div>
                                        <div  class="col-sm-4  mb-2">
                                            <label>Father Name:</label>
                                            <div >
                                                <input class="form-control " v-model="employee.fatherName" />
                                               
                                            </div>
                                        </div>
                                        <div v-if="isOtherLang()" class="col-sm-4  mb-2">
                                            <label>{{$arabicLanguage($t('AddEmployeeRegistration.EmployeeName(Arabic)'))  }}:<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.employee.arabicName.$error}">
                                                <input class="form-control " v-bind:class="isLeftToRight() ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.arabicName.$model" />
                                                <span v-if="v$.employee.arabicName.$error" class="error text-danger arabicLanguage">
                                                    <span class="arabicLanguage" v-if="!v$.employee.arabicName.required">{{ $t('AddEmployeeRegistration.ArValidation') }}</span>
                                                    <span class="arabicLanguage" v-if="!v$.employee.arabicName.maxLength">{{ $t('AddEmployeeRegistration.ArMax') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4  mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.Designation') }}:</label>
                                            <div>
                                                <designationDropdown v-model="employee.designationId" :modelValue="employee.designationId"></designationDropdown>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.Department') }}:</label>
                                            <div>
                                                <departmentDropdown v-model="employee.departmentId" :modelValue="employee.departmentId"></departmentDropdown>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.Email') }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.employee.email.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" type="email" v-model="v$.employee.email.$model" @blur="EmailDuplicate(employee.email)" />
                                                <span v-if="v$.employee.email.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.email.required">{{ $t('AddEmployeeRegistration.EmailRequired') }}</span>
                                                    <span v-if="!v$.employee.email.email">{{ $t('AddEmployeeRegistration.EmailInvalid') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.IDNumber') }}:<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.employee.idNumber.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" type="number" v-model="v$.employee.idNumber.$model" />
                                                <span v-if="v$.employee.idNumber.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.idNumber.required">{{ $t('AddEmployeeRegistration.IDRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.EmGender') }} :<span class="text-danger"> *</span></label>
                                            <div v-bind:class="{'has-danger' : v$.employee.gender.$error}">
                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" v-model="v$.employee.gender.$model" :options="['Male', 'Fe-Male', 'Other']" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :show-labels="false" :placeholder="$t('AddEmployeeRegistration.SelectGender')">
                                                </multiselect>
                                                <multiselect v-else v-model="v$.employee.gender.$model" :options="['ذكر', 'أنثى', 'آخر']" :show-labels="false" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :placeholder="$t('AddEmployeeRegistration.SelectGender')">
                                                </multiselect>
                                                <span v-if="v$.employee.gender.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.gender.required">{{ $t('AddEmployeeRegistration.GValidation') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>Employee Avalibility :</label>
                                            <multiselect  v-model="employee.employeeAccess" :options="['Full-Time','Part-Time', 'Remote','Hybrid']"  :show-labels="false" placeholder="Select Option">
                                                </multiselect>
                                                <!-- <multiselect v-else v-model="employee.employeeAccess"  :options="['ذكر', 'أنثى', 'آخر']" :show-labels="false" placeholder="Select Option">
                                                </multiselect> -->
                                        </div>
                                        <!--<div class="col-sm-4">
                        <label>{{ $t('Employee.DateOf') }} :</label>
                        <div v-bind:class="{'has-danger' : v$.employee.dateOfBirth.$error}">
                            <datepicker v-model="v$.employee.dateOfBirth.$model" :key="daterander"></datepicker>
                            <span v-if="v$.employee.dateOfBirth.$error" class="error text-danger">
                            </span>
                        </div>
                    </div>-->
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.MartialStatus') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.martialStatus.$error}">
                                                <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" v-model="v$.employee.martialStatus.$model" :options="['Single', 'Married', 'Divorced']" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :show-labels="false" :placeholder="$t('AddEmployeeRegistration.SelectMartialStatus')">
                                                </multiselect>

                                                <multiselect v-else v-model="v$.employee.martialStatus.$model" :options="['غير مرتبطة', 'متزوج', 'مطلق']" :show-labels="false" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" :placeholder="$t('AddEmployeeRegistration.SelectMartialStatus')">
                                                </multiselect>
                                            </div>
                                        </div>
                                     
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.Nationality') }} :</label>
                                            <div>
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="employee.nationality" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.DateOf') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.dateOfBirth.$error}">
                                                <datepicker v-model="v$.employee.dateOfBirth.$model" :key="daterander"></datepicker>
                                                <span v-if="v$.employee.dateOfBirth.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.MobileNo') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.mobileNo.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" type="number" v-model="v$.employee.mobileNo.$model" />
                                                <span v-if="v$.employee.mobileNo.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.mobileNo.maxLength">{{ $t('AddEmployeeRegistration.MobLength') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.OtherContact') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.otherContact.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" type="number" v-model="v$.employee.otherContact.$model" />
                                                <span v-if="v$.employee.otherContact.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.otherContact.required"> {{ $t('AddEmployeeRegistration.OtherContactRequired') }}</span>
                                                </span>
                                            </div>
                                        </div>

                                        <div class="col-sm-4 mb-2 form-group">
                                            <label>{{ $t('AddEmployeeRegistration.BloodGroup') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.bloodGroup.$error}">
                                                <multiselect v-model="v$.employee.bloodGroup.$model" :options="['A+', 'A-', 'B+', 'B-', 'O+', 'O-', 'AB+', 'AB-']" :show-labels="false" :placeholder="$t('AddEmployeeRegistration.SelectBloodGroup')">
                                                </multiselect>
                                            </div>
                                        </div>

                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.National/Foreign') }} :</label>
                                            <multiselect v-if="($i18n.locale == 'en' ||isLeftToRight())" v-model="employee.nationalOrForeign" :options="['National', 'Foreign']"  :show-labels="false" :placeholder="$t('AddEmployeeRegistration.Select')">
                                            </multiselect>

                                            <multiselect v-else v-model="employee.nationalOrForeign" :options="['غیر ملکی', 'قومی']" :show-labels="false"  :placeholder="$t('AddEmployeeRegistration.Select')">
                                            </multiselect>
                                        </div>

                                        <div class="col-sm-4 mb-2 form-group">
                                            <label>{{ $t('AddEmployeeRegistration.Country') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.country.$error}">
                                                <countryfor-employee-dropdown v-model="v$.employee.country.$model" @update:modelValue="SetCity()" :modelValue="employee.country" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4 mb-2 form-group">
                                            <label>{{ $t('AddEmployeeRegistration.City') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.city.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.city.$model" />
                                                <!--<city-for-employee-dropdown v-model="v$.employee.city.$model" modelValue="employee.city" :country="employee.country" :key="cityRender" />-->
                                            </div>
                                        </div>

                                        <div class="col-sm-4 mb-2 form-group" v-if="employee.martialStatus=='Married' || employee.martialStatus=='متزوج'">
                                            <label>Spouse Name 1:</label>
                                            <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="employee.spouseName1" />
                                        </div>

                                        <div class="col-sm-4 mb-2 form-group" v-if="employee.martialStatus=='Married' || employee.martialStatus=='متزوج'">
                                            <label>Spouse Name 2:</label>
                                            <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="employee.spouseName2" />
                                        </div>

                                        <div class="col-sm-4 mb-2 form-group" v-if="employee.martialStatus=='Married' || employee.martialStatus=='متزوج'">
                                            <label>Spouse Name 3:</label>
                                            <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="employee.spouseName3" />
                                        </div>

                                        <div class="col-sm-4 mb-2 form-group" v-if="employee.martialStatus=='Married' || employee.martialStatus=='متزوج'">
                                            <label>Spouse Name 4:</label>
                                            <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="employee.spouseName4" />
                                        </div>
                                    </div>


                                    <div class="row">
                                        <div class="col-sm-12 form-group">
                                            <label>{{ $t('AddEmployeeRegistration.Address') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.address.$error}">
                                                <textarea class="form-control " rows="3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.address.$model" />
                                                <span v-if="v$.employee.address.$error" class="error text-danger">
                                                    <span v-if="!v$.employee.address.maxLength">{{ $t('AddEmployeeRegistration.AddressMaximum') }}</span>
                                                </span>
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-3">
                                        <div class="checkbox form-check-inline mx-2">
                                                <input type="checkbox" id="inlineCheckbox1" v-model="employee.isActive">
                                                <label for="inlineCheckbox1"> {{ $t('AddCustomer.Active') }}  </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div v-if="active == 'Salary'">
                                <div class="card-body ">

                                    <div class="row">
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.SalaryType') }}</label>
                                            <multiselect v-model="employee.salaryType" :options="salaryTypeOptions" :searchable="false" :allow-empty="false"  v-bind:placeholder="$t('AddEmployeeRegistration.Select')" :show-labels="false">
                                            </multiselect>
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>{{ $t('AddEmployeeRegistration.PerDayWorkHour') }} :</label>
                                            <input class="form-control" type="number" v-bind:class="$i18n.locale == 'en' ? 'text-left' : 'arabicLanguage'" v-model="employee.perDayWorkHour" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div v-if="active == 'Bank'">
                                <div class="card-body ">

                                    <div class="row">
                                        
                                        <div class="col-sm-4 mb-2">
                                            <label>Bank Name :</label>
                                            <input class="form-control" type="text" v-model="employee.bankName" />
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>Branch Name :</label>
                                            <input class="form-control" type="text" v-model="employee.branchName" />
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>Account Name :</label>
                                            <input class="form-control" type="text" v-model="employee.accountName" />
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>Account Number :</label>
                                            <input class="form-control" type="text" v-model="employee.accountNumber" />
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>IBAN Number :</label>
                                            <input class="form-control" type="text" v-model="employee.ibanNumber" />
                                        </div>
                                        <div class="col-sm-4 mb-2">
                                            <label>Account Type :</label>
                                            <input class="form-control" type="text" v-model="employee.accountType" />
                                        </div>
                                        <div class="col-sm-8 mb-2">
                                            <label>Bank Address :</label>
                                            <textarea class="form-control" type="text" rows="3" v-model="employee.bankAddress" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div v-if="active == 'Emergency'">

                                <div class="card-body ">
                                    <div class="row ">
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.NameofPerson') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.primaryNameOfPerson.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.primaryNameOfPerson.$model" />
                                                <span v-if="v$.employee.primaryNameOfPerson.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.Relation') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.primaryRelation.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.primaryRelation.$model" />
                                                <span v-if="v$.employee.primaryRelation.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.ContactNumber') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.primaryContactNumber.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.primaryContactNumber.$model" />
                                                <span v-if="v$.employee.primaryContactNumber.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.Email') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.primaryReferenceEmail.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.primaryReferenceEmail.$model" />
                                                <span v-if="v$.employee.primaryReferenceEmail.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.NameofPerson(Secondary)') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.secondaryNameOfPerson.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.secondaryNameOfPerson.$model" />
                                                <span v-if="v$.employee.secondaryNameOfPerson.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.Relation2') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.secondaryRelation.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.secondaryRelation.$model" />
                                                <span v-if="v$.employee.secondaryRelation.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.ContactNumber2') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.secondaryContactNumber.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.secondaryContactNumber.$model" />
                                                <span v-if="v$.employee.secondaryContactNumber.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.SecondaryReferenceEmail') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.secondaryReferenceEmail.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.secondaryReferenceEmail.$model" />
                                                <span v-if="v$.employee.secondaryReferenceEmail.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div v-if="active == 'HomeCountry'">

                                <div class="card-body ">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.NameofPerson2') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.homePersonName.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.homePersonName.$model" />
                                                <span v-if="v$.employee.homePersonName.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.Relation2') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.homeRelation.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.homeRelation.$model" />
                                                <span v-if="v$.employee.homeRelation.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.ContactNumber') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.homeContactNumber.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.homeContactNumber.$model" />
                                                <span v-if="v$.employee.homeContactNumber.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.Email') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.homeReferenceEmail.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.homeReferenceEmail.$model" />
                                                <span v-if="v$.employee.homeReferenceEmail.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.City') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.homeCity.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.homeCity.$model" />
                                                <span v-if="v$.employee.homeCity.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.Country') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.homeCountry.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.homeCountry.$model" />
                                                <span v-if="v$.employee.homeCountry.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div v-if="active == 'LegalIdentity'">

                                <div class="card-body ">
                                    <div class="row ">

                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.Type') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.idType.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.idType.$model" />
                                                <span v-if="v$.employee.idType.$error" class="error text-danger">

                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.Title') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.title.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.title.$model" />
                                                <span v-if="v$.employee.title.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.IDExpiry') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.expiryDate.$error}">
                                                <datepicker v-model="v$.employee.expiryDate.$model" :key="daterander"></datepicker>
                                                <span v-if="v$.employee.expiryDate.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.PassportNumber') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.passportNumber.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.passportNumber.$model" />
                                                <span v-if="v$.employee.passportNumber.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.PassportIssueDate') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.passportIssueDate.$error}">
                                                <datepicker v-model="v$.employee.passportIssueDate.$model" :key="daterander"></datepicker>
                                                <span v-if="v$.employee.passportIssueDate.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.PassportExpiryDate') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.passportExpiryDate.$error}">
                                                <datepicker v-model="v$.employee.passportExpiryDate.$model" :key="daterander"></datepicker>
                                                <span v-if="v$.employee.passportExpiryDate.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.IssuingAuthority') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.passportIssuingAuthority.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.passportIssuingAuthority.$model" />
                                                <span v-if="v$.employee.passportIssuingAuthority.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.DrivingLicenseNo') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.drivingLicenseNumber.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.drivingLicenseNumber.$model" />
                                                <span v-if="v$.employee.drivingLicenseNumber.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.LicenseType') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.drivingLicenseType.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.drivingLicenseType.$model" />
                                                <span v-if="v$.employee.drivingLicenseType.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.IssuingAuthority') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.drivingIssuingAuthority.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.drivingIssuingAuthority.$model" />
                                                <span v-if="v$.employee.drivingIssuingAuthority.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.LicenseExpiry') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.drivingExpiryDate.$error}">
                                                <datepicker v-model="v$.employee.drivingExpiryDate.$model" :key="daterander"></datepicker>
                                                <span v-if="v$.employee.drivingExpiryDate.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.MedicalPolicyNo') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.medicalPolicNumber.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.medicalPolicNumber.$model" />
                                                <span v-if="v$.employee.medicalPolicNumber.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.MedicalPolicyType') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.medicalPolicType.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.medicalPolicType.$model" />
                                                <span v-if="v$.employee.medicalPolicType.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.MedicalPolicyProvider') }} :</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.medicalPolicProvider.$error}">
                                                <input class="form-control " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="v$.employee.medicalPolicProvider.$model" />
                                                <span v-if="v$.employee.medicalPolicProvider.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <label>{{ $t('AddEmployeeRegistration.MedicalPolicyExpiry') }}:</label>
                                            <div v-bind:class="{'has-danger' : v$.employee.medicalPolicExpiryDate.$error}">
                                                <datepicker v-model="v$.employee.medicalPolicExpiryDate.$model" :key="daterander"></datepicker>
                                                <span v-if="v$.employee.medicalPolicExpiryDate.$error" class="error text-danger">
                                                </span>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div v-if="active == 'Attachment'">
                                <div v-if="(employee.employeeAttachments[0].cnic == '' &&
                                                 employee.employeeAttachments[0].photo == '' &&
                                                 employee.employeeAttachments[0].drivingLicense == '' &&
                                                 employee.employeeAttachments[0].passport == '') || isAddAttachment">


                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label>{{ $t('AddEmployeeRegistration.CNIC') }}:</label>
                                                <div>
                                                    <span>
                                                        <input ref="imgupload1" type="file" id="file-input"
                                                               @change="uploadImage('cnic')"
                                                               name="image" style="opacity:1;padding:25px">
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <label>{{ $t('AddEmployeeRegistration.DrivingLicense') }}:</label>
                                                <div>
                                                    <span>
                                                        <input ref="imgupload2" type="file" id="file-input"
                                                               @change="uploadImage('drivingLicense')" name="image" style="opacity:1;padding:25px">
                                                    </span>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <label>{{ $t('AddEmployeeRegistration.Passport') }} :</label>
                                                <div>
                                                    <span>
                                                        <input ref="imgupload3" type="file" id="file-input"
                                                               @change="uploadImage('passport')"
                                                               name="image" style="opacity:1;padding:25px">
                                                    </span>
                                                </div>
                                            </div>

                                            <div class="col-sm-6">
                                                <label>{{ $t('AddEmployeeRegistration.Photo') }} :</label>
                                                <div>
                                                    <span>
                                                        <input ref="imgupload4" type="file" id="file-input"
                                                               @change="uploadImage('photo')"
                                                               name="image" style="opacity:1;padding:25px">
                                                    </span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="" v-else>
                                    <div class="col-md-12">
                                        <div class="ml-2 mt-2 col-sm- float-left">
                                            <h5 class="card-title ">{{ $t('AddEmployeeRegistration.EmployeeAttachment') }}</h5>
                                        </div>
                                        <div class="col-sm-6 float-right">
                                            <a href="javascript:void(0)" class="btn btn-outline-primary  float-right" v-on:click="isAddAttachment=true"><i class="fa fa-upload"></i> Upload</a>
                                        </div>
                                    </div>

                                    <div class="card-body">
                                        <div class=" table-responsive">
                                            <table class="table ">
                                                <thead class="m-0">
                                                    <tr>
                                                        <th>#</th>
                                                        <th>{{ $t('AddEmployeeRegistration.Date') }}</th>
                                                        <th>{{ $t('AddEmployeeRegistration.CNIC') }} </th>
                                                        <th>{{ $t('AddEmployeeRegistration.Photo') }}</th>
                                                        <th>{{ $t('AddEmployeeRegistration.DrivingLicense') }}</th>
                                                        <th>{{ $t('AddEmployeeRegistration.Passport') }}</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                    <tr v-for="(emp,index) in employee.employeeAttachments" v-bind:key="index">
                                                        <td>
                                                            {{index+1}}
                                                        </td>
                                                        <th>{{$formatDate(emp.date)  }}</th>
                                                        <td>
                                                            <button class="btn btn-primary  btn-icon mr-2"
                                                                    v-if="emp.cnic != ''"
                                                                    v-on:click="DownloadAttachment(emp.cnic)">
                                                                <i class="fa fa-download"></i>
                                                            </button>
                                                        </td>
                                                        <td>
                                                            <button class="btn btn-primary  btn-icon mr-2"
                                                                    v-if="emp.photo != ''"
                                                                    v-on:click="DownloadAttachment(emp.photo)">
                                                                <i class="fa fa-download"></i>
                                                            </button>
                                                        </td>
                                                        <td>
                                                            <button class="btn btn-primary  btn-icon mr-2"
                                                                    v-if="emp.drivingLicense != ''"
                                                                    v-on:click="DownloadAttachment(emp.drivingLicense)">
                                                                <i class="fa fa-download"></i>
                                                            </button>
                                                        </td>
                                                        <td>
                                                            <button class="btn btn-primary  btn-icon mr-2"
                                                                    v-if="emp.passport != ''"
                                                                    v-on:click="DownloadAttachment(emp.passport)">
                                                                <i class="fa fa-download"></i>
                                                            </button>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="card-footer">
                                        <div class="float-left">
                                            <span v-if="currentPage===1 && rowCount === 0">  {{ $t('Pagination.ShowingEntries') }}</span>
                                            <span v-else-if="currentPage===1 && rowCount < 5">  {{ $t('Pagination.Showing') }} {{currentPage}}  {{ $t('Pagination.to') }} {{rowCount}}  {{ $t('Pagination.of') }} {{rowCount}}  {{ $t('Pagination.entries') }}</span>
                                            <span v-else-if="currentPage===1 && rowCount >= 6  "> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                            <span v-else-if="currentPage===1"> {{ $t('Pagination.Showing') }} {{currentPage}} {{ $t('Pagination.to') }} {{currentPage*10}} of {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                            <span v-else-if="currentPage!==1 && currentPage!==pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{currentPage*10}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                            <span v-else-if="currentPage === pageCount"> {{ $t('Pagination.Showing') }} {{(currentPage*10)-9}} {{ $t('Pagination.to') }} {{rowCount}} {{ $t('Pagination.of') }} {{rowCount}} {{ $t('Pagination.entries') }}</span>
                                        </div>
                                        <div class="float-right">
                                            <div class="overflow-auto" v-on:click="GetEmployeeAttachments(search, currentPage, employee.id)">
                                                <b-pagination pills size="lg" v-model="currentPage"
                                                              :total-rows="rowCount"
                                                              :per-page="5"
                                                              first-text="First"
                                                              prev-text="Previous"
                                                              next-text="Next"
                                                              last-text="Last"></b-pagination>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div v-if="active == 'TemporaryCashRequest'">

                                <div class="card-body ">
                                    <div class="row ">

                                        <div class="col-sm-6">
                                            <label>{{ $t('AddSignUp.TemporaryCashReceiver') }}:</label> <br />
                                            <toggle-button v-model="employee.temporaryCashReceiver" color="#3178F6" class="mr-2 ml-2" style="z-index:0 !important" />
                                        </div>

                                        <div class="col-sm-6">
                                            <label>{{ $t('AddSignUp.TemporaryCashIssuer') }}:</label> <br />
                                            <toggle-button v-model="employee.temporaryCashIssuer" color="#3178F6" class="mr-2 ml-2" style="z-index:0 !important" />
                                        </div>

                                        <div class="col-sm-6">
                                            <label>{{ $t('AddSignUp.TemporaryCashRequester') }}:</label> <br />
                                            <toggle-button v-model="employee.temporaryCashRequester" color="#3178F6" class="mr-2 ml-2" style="z-index:0 !important" />
                                        </div>

                                        <div class="col-sm-6" v-if="employee.temporaryCashReceiver">
                                            <label>{{ $t('AddSignUp.Days') }}:</label>
                                            <input class="form-control" type="number" v-model="employee.days" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />

                                        </div>
                                        <div class="col-sm-6" v-if="employee.temporaryCashReceiver">
                                            <label>{{ $t('AddSignUp.Limit') }}:</label>
                                            <input class="form-control" type="number" v-model="employee.limit" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" />

                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <div class="col-lg-12 mt-4 ">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-8" style="border-right: 1px solid #eee;">

                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group ps-3">
                                                <div class="font-xs mb-1">{{ $t('AddEmployeeRegistration.AttachFiles') }}</div>
                                                <button v-on:click="Attachment()" type="button" class="btn btn-light btn-square btn-outline-dashed mb-1"><i class="fas fa-cloud-upload-alt"></i> {{ $t('PurchaseView.Attachment') }} </button>
                                                <div>
                                                    <small class="text-muted">
                                                        {{ $t('AddEmployeeRegistration.FileSize') }}
                                                    </small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div><!--end row-->

                </div>
               
                <div v-if="!loading" class="card-footer">
                    <div class="row">
                        <div class="button-items">
                            <button class="btn btn-outline-primary  " :disabled="v$.employee.$invalid" v-on:click="SaveEmployee" v-if="employee.id=='00000000-0000-0000-0000-000000000000' && isValid('CanAddEmployeeReg') "><i class="fas fa-save me-2"></i> {{ $t('AddEmployeeRegistration.btnSave') }}</button>
                            <button class="btn btn-outline-primary  " :disabled="v$.employee.$invalid" v-on:click="SaveEmployee" v-if="employee.id!='00000000-0000-0000-0000-000000000000' && isValid('CanEditEmployeeReg') "><i class="fas fa-save me-2"></i> {{ $t('AddEmployeeRegistration.btnUpdate') }}</button>

                            <button class="btn btn-danger  " v-on:click="Cancel">{{ $t('AddEmployeeRegistration.btnClear') }}</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <bulk-attachment :attachmentList="employee.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
     <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
    </div>
    <div v-else> <acessdenied></acessdenied></div>

</template>
<script>
    // import { required, maxLength, email, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import { required, maxLength, email, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Multiselect from 'vue-multiselect';
    import moment from 'moment';
      import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    import clickMixin from '@/Mixins/clickMixin'
    export default ({
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
          setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                IsPaksitanClient: false,
                arabic: '',
                english: '',
                value: '',
                loading: false,
                active: 'PersonalInfo',
                language: 'Nothing',
                daterander: 0,
                salaryTypeOptions: [],
                employee: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',
                    registrationDate: '',
                    englishName: '',
                    fatherName: '',
                    salaryType: '',
                    arabicName: '',
                    gender: '',
                    martialStatus: '',
                    employeeType: '',
                    nationality: '',
                    dateOfBirth: '',
                    mobileNo: '',
                    otherContact: '',
                    email: '',
                    bloodGroup: '',
                    city: '',
                    isSignup: false,
                    isActive: true,
                    country: '',
                    address: '',
                    primaryNameOfPerson: '',
                    primaryRelation: '',
                    primaryContactNumber: '',
                    primaryReferenceEmail: '',
                    secondaryNameOfPerson: '',
                    secondaryRelation: '',
                    secondaryContactNumber: '',
                    secondaryReferenceEmail: '',
                    homePersonName: '',
                    homeRelation: '',
                    homeContactNumber: '',
                    homeReferenceEmail: '',
                    homeCity: '',
                    homeCountry: '',
                    idNumber: '',
                    idType: '',
                    title: '',
                    expiryDate: '',
                    passportNumber: '',
                    passportIssueDate: '',
                    passportExpiryDate: '',
                    passportIssuingAuthority: '',
                    drivingLicenseNumber: '',
                    drivingLicenseType: '',
                    drivingExpiryDate: '',
                    drivingIssuingAuthority: '',
                    medicalPolicNumber: '',
                    medicalPolicType: '',
                    medicalPolicProvider: '',
                    medicalPolicExpiryDate: '',
                    cnic: '',
                    photo: '',
                    drivingLicense: '',
                    passport: '',
                    designationId: '',
                    departmentId: '',
                    reason: '',
                    bankName: '',
                    AccountName: '',
                    IbanNumber: '',
                    accountNumber: '',
                    branchName: '',
                    bankAddress: '',
                    accountType: '',
                    employeeAccess: '',
                    perDayWorkHour: 0,
                    employeeAttachments: [{
                        cnic: '',
                        photo: '',
                        drivingLicense: '',
                        passport: ''
                    }],
                    nationalOrForeign: '',
                    spouseName1: '',
                    spouseName2: '',
                    spouseName3: '',
                    spouseName4: '',
                    days: 0,
                    limit: 0,
                    temporaryCashReceiver: false,
                    temporaryCashIssuer: false,
                    temporaryCashRequester: false,
                    attachmentList: [],
                },
                currentPage: 1,
                pageCount: '',
                rowCount: '',
                search: '',
                isAddAttachment: false,
                emailExist: false,
                show: false,
                cityRender: 0,
            }
        },
        validations() {
           return{
             employee:
            {
                code: { required },
                registrationDate: { required },
                englishName: {
                    maxLength: maxLength(30)
                },
                arabicName: {
                     required: requiredIf(function () {
                            return !this.employee.englishName;
                        }),
                    // required: requiredIf((x) => {
                    //     if (x.englishName == '' || x.englishName == null)
                    //         return true;
                    //     return false;
                    // }),
                    maxLength: maxLength(40)
                },
                gender: { required },
                martialStatus: {},
                employeeType: {},
                dateOfBirth: {},
                mobileNo: { maxLength: maxLength(25) },
                email: { required, email },
                address: { maxLength: maxLength(350) },
                idNumber: { required },

                bloodGroup: {},
                otherContact: {},
                city: {},
                country: {},
                nationality: {},
                primaryNameOfPerson: {},
                primaryRelation: {},
                primaryContactNumber: {},
                primaryReferenceEmail: {},
                secondaryNameOfPerson: {},
                secondaryRelation: {},
                secondaryContactNumber: {},
                secondaryReferenceEmail: {},
                homePersonName: {},
                homeRelation: {},
                homeContactNumber: {},
                homeReferenceEmail: {},
                homeCity: {},
                homeCountry: {},
                idType: {},
                title: {},
                expiryDate: {},
                passportNumber: {},
                passportIssueDate: {},
                passportExpiryDate: {},
                passportIssuingAuthority: {},
                drivingLicenseNumber: {},
                drivingLicenseType: {},
                drivingExpiryDate: {},
                drivingIssuingAuthority: {},
                medicalPolicNumber: {},
                medicalPolicType: {},
                medicalPolicProvider: {},
                medicalPolicExpiryDate: {}
            }
           }
        },
        methods: {
            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.employee.attachmentList = attachment;
                this.show = false;
            },

            SetCity: function () {
                this.cityRender++;
            },
            ChangeCode: function () {
            
            var value;
            if (this.employee.employeeType == 'Permanent') {
                value = 'Pr';
            }
            else if (this.employee.employeeType == 'Probation') {
                value = 'Pb';
            }
            else if (this.employee.employeeType == 'Internee') {
                value = 'In';
            }
            else if (this.employee.employeeType == 'Temporary') {
                value = 'Tm';
            }
            let str = this.employee.code;
            if (str.indexOf("-") !== -1 && str.indexOf("-", str.indexOf("-") + 1) !== -1) 
            {
               let originalString = str;
                let regex = /-(.*?)-/; // matches any text between underscores
                let replacementText = value;

                let modifiedString = originalString.replace(regex, "-" + replacementText  + "-");
                this.employee.code=modifiedString;
            }
           
            else {
                let splitIndex = this.employee.code.indexOf("-") + 1; // Find the index of "-" and add 1 to include it in the second part
                this.employee.code = this.employee.code.slice(0, splitIndex) + value + '-' + this.employee.code.slice(splitIndex);
            }


        },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.employee.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addEmployeeRegistration');
                    }
                    else {

                        this.$swal({
                            title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You cannot Change language during update, otherwise your current page data will be lose!' : 'لا يمكنك تغيير اللغة أثناء التحديث ، وإلا ستفقد بيانات صفحتك الحالية!',
                            type: 'error',
                            confirmButtonClass: "btn btn-danger",
                            icon: 'error',
                            timer: 4000,
                            timerProgressBar: true,
                        });
                    }
                }


            },
            makeActive: function (tab) {

                this.active = tab;
            },
            EmailDuplicate: function (x) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/account/DuplicateEmail?email=' + x, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data == true) {
                            root.emailExist = true;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Email Already Exist!' : 'البريد الالكتروني موجود مسبقا!',
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1700,
                                timerProgressBar: true,
                            });
                        }
                        else {
                            root.emailExist = false;
                        }


                    })
            },

            uploadImage(type) {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = null;

                if (type == 'cnic') {
                    file = this.$refs.imgupload1.files;
                }

                if (type == 'drivingLicense') {
                    file = this.$refs.imgupload2.files;
                }

                if (type == 'passport') {
                    file = this.$refs.imgupload3.files;
                }

                if (type == 'photo') {
                    file = this.$refs.imgupload4.files;
                }

                var fileData = new FormData();
                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }
                root.$https.post('/company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {
                            if (type == 'cnic') {
                                root.employee.cnic = response.data;
                            }

                            if (type == 'drivingLicense') {
                                root.employee.drivingLicense = response.data;
                            }

                            if (type == 'passport') {
                                root.employee.passport = response.data;
                            }

                            if (type == 'photo') {
                                root.employee.photo = response.data;
                            }
                        }
                    });
            },

            DownloadAttachment(path) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                var ext = path.split('.')[1];
                root.$https.get('/contact/DownloadFile?filePath=' + path, { headers: { "Authorization": `Bearer ${token}` }, responseType: 'blob' })
                    .then(function (response) {
                        const url = window.URL.createObjectURL(new Blob([response.data]));
                        const link = document.createElement('a');
                        link.href = url;
                        link.setAttribute('download', 'file.' + ext);
                        document.body.appendChild(link);
                        link.click();
                    });
            },
            GetAutoCodeGenerator: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https
                    .get('/EmployeeRegistration/EmployeeCode', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                        if (response.data != null) {
                            root.employee.code = response.data;
                        }
                    });
            },
            Cancel: function () {
                if (this.isValid('CanViewEmployeeReg')) {
                    this.$router.push({
                        path: '/employeeRegistration',

                    })
                }
                else {
                    this.$router.go();
                }
            },


            SaveEmployee: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if (this.emailExist) {
                    this.$swal({
                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                        text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Email Already Exist!' : 'البريد الالكتروني موجود مسبقا!',
                        type: 'error',
                        icon: 'error',
                        showConfirmButton: false,
                        timer: 1700,
                        timerProgressBar: true,
                    });
                }
                else {
                    if (this.employee.employeeType == '' || this.employee.employeeType == undefined) {
                        this.employee.employeeType = 'default';

                    }
                    root.$https
                        .post('/EmployeeRegistration/SaveEmployee', this.employee, { headers: { "Authorization": `Bearer ${token}` } })
                        .then(response => {
                            this.loading = false
                            this.info = response.data.bpi

                            this.$swal.fire({
                                icon: 'success',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully' : 'حفظ بنجاح',
                                text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved' : 'تم الحفظ',

                                showConfirmButton: false,
                                timer: 1800,
                                timerProgressBar: true,

                            });
                            if (root.isValid('CanViewEmployeeReg')) {
                                root.$router.push({
                                    path: '/employeeRegistration',

                                })
                            }
                            else {
                                root.$router.go();
                            }
                        })
                        .catch(error => {
                            console.log(error)
                            root.$swal.fire(
                                {
                                    icon: 'error',
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Server Error!' : 'خطأ في الخادم',
                                    text: error.response.data,
                                    showConfirmButton: false,
                                    timer: 5000,
                                    timerProgressBar: true,
                                });

                            root.loading = false
                        })
                        .finally(() => root.loading = false)
                }
            },

            GetEmployeeAttachments: function (search, currentPage, id) {
                var root = this;
                var url = '/EmployeeRegistration/GetEmployeeAttachments?searchTerm=' + search + '&pageNumber=' + currentPage + '&id=' + id;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                root.$https.get(url, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.results.length > 0) {

                        root.employee.employeeAttachments = response.data.results;
                        root.employee.cnic = response.data.results[0].cnic;
                        root.employee.photo = response.data.results[0].photo;
                        root.employee.drivingLicense = response.data.results[0].drivingLicense;
                        root.employee.passport = response.data.results[0].passport;
                    }
                    else {
                        root.employee.employeeAttachments = [{
                            cnic: '',
                            photo: '',
                            drivingLicense: '',
                            passport: ''
                        }]

                    }

                    root.pageCount = response.data.pageCount;
                    root.rowCount = response.data.rowCount;

                });
            }
        },
        created: function () {
            var root =  this;

            if(root.$route.query.Add == 'false')
            {
                root.$route.query.data = this.$store.isGetEdit;
            }
            this.$emit('update:modelValue', this.$route.name);
            if (this.$route.query.data != undefined) {
                this.employee = this.$route.query.data;
                this.GetEmployeeAttachments(this.search, 1, this.employee.id);

                this.daterander++;
            }
            else {
                this.employee.salaryType = (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Bank' : 'بنكي';
            }
        },
        filters: {
            formatDate: function (value) {
                return moment(value).format("DD MMM yyyy hh:mm");
            }
        },
        mounted: function () {
            this.language = this.$i18n.locale;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.IsPaksitanClient = localStorage.getItem('IsPaksitanClient') == "true" ? true : false;

            if (this.$route.query.data == undefined || this.$route.query.data == '') {
                //eslint-dsiable-line
                this.GetAutoCodeGenerator();
                this.employee.registrationDate = moment().format('llll');
            }

            if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                this.salaryTypeOptions = ['Cash', 'Bank'];
            }
            else {
                this.salaryTypeOptions = ['الـنـقـدي', 'بنكي'];
            }
        }
    })

</script>