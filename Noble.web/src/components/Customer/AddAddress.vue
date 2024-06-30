<template>
    <modal show="show" :modalLarge="true">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">Edit Address</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>Register Address</h6>
                <button type="button" class="btn-close" v-if="isSale==true" v-on:click="close()" data-bs-toggle="offcanvas" ref="offcanvasRight" data-bs-target="#offcanvasRight"></button>
                <button type="button" class="btn-close" v-else v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">

                    <div class="form-group has-label col-sm-4 " v-if="isSale==true">
                        <label class="text  font-weight-bolder">Address For:<span class="text-danger"> *</span> </label>
                        <multiselect v-model="address.type" disabled :options="['Billing','Delivery', 'Shipping', 'National']" :show-labels="false" placeholder="Address For">
                        </multiselect>
                    </div>
                    <div class="form-group has-label col-sm-4 " v-else>
                        <label class="text  font-weight-bolder">Address For:<span class="text-danger"> *</span> </label>
                        <multiselect @update:modelValue="GetAddressFor()" v-model="address.type" :options="['Billing','Delivery', 'Shipping', 'National']" :show-labels="false" placeholder="Address For">
                        </multiselect>
                    </div>
                    <div class="form-group has-label col-sm-4 ">
                        <label class="text  font-weight-bolder">Location Type:<span class="text-danger"> *</span> </label>
                        <multiselect v-model="address.area" :options="[ 'Apartment','Home','Office','Shop','Villa','Warehouse','Other']" :show-labels="false" :placeholder="$t('AddCustomer.SelectType')">
                        </multiselect>
                    </div>

                    <div class="form-group has-label col-sm-6 ">
                        <label class="text  font-weight-bolder"> Address Line 1:<span class="text-danger"> *</span> </label>
                        <textarea rows="3" class="form-control" v-model="v$.address.address.$model" type="text" />
                        <span v-if="v$.address.address.$error" class="error">
                            <span v-if="!v$.address.address.required">Address is Required</span>
                        </span>
                    </div>
                    <div class="form-group has-label col-sm-6">
                        <label class="text  font-weight-bolder"> Address Line 2: </label>
                        <textarea rows="3" class="form-control" v-model="address.address2" type="text" />


                    </div>
                    <div class="form-group has-label col-sm-4 ">
                        <label class="text  font-weight-bolder"> City:  </label>
                        <input class="form-control" v-model="address.city" type="text" />
                        
                    </div>
                    <div class="form-group has-label col-sm-4 ">
                        <label class="text  font-weight-bolder"> Country:  </label>
                        <countrydropdown v-model="address.country"></countrydropdown>

                        
                    </div>
                    <div class="form-group has-label col-sm-4 ">
                        <label class="text  font-weight-bolder"> Zip/Postal Code:  </label>
                        <input class="form-control" v-model="address.billingZipCode" type="text" />
                        
                    </div>
                    <div class="form-group has-label col-sm-6 ">
                        <label class="text  font-weight-bolder"> Contact Name:<span class="text-danger"> *</span>  </label>
                        <input class="form-control" v-model="v$.address.contactName.$model" type="text" />
                        <span v-if="v$.address.contactName.$error" class="error">
                            <span v-if="!v$.address.contactName.required">Contact Name is Required</span>
                        </span>
                    </div>
                    <div class="form-group has-label col-sm-6 ">
                        <label class="text  font-weight-bolder"> Contact Number:<span class="text-danger"> *</span>  </label>
                        <input class="form-control" v-model="v$.address.contactNumber.$model" type="text" />
                        <span v-if="v$.address.contactNumber.$error" class="error">
                            <span v-if="!v$.address.contactNumber.required">Contact Number is Required</span>
                        </span>
                    </div>
                    <div class="row">
                        <div class="col-8">
                            <div class="row">
                                <div class="col-6">
                                    <b style="padding-bottom: 5px !important;">Preffered Delivery Time & Day</b>

                                </div>
                                <div class="col-6">
                                    <div class="form-check form-switch form-switch-dark">
                                        <input class="form-check-input" type="checkbox" id="customSwitchSecondary" @update:modelValue="GetTime" v-model="address.allHour">
                                        <label class="form-check-label" for="customSwitchSecondary">24 Hour Availability</label>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-4">

                            <div class="form-check form-switch form-switch-dark">
                                <input class="form-check-input" type="checkbox" id="customSwitchSecondary" @update:modelValue="AllDaySelection()" v-model="address.allDaySelection">
                                <label class="form-check-label" for="customSwitchSecondary">All DAY</label>
                            </div>

                        </div>

                        <div class="form-group has-label col-sm-4 ">
                            <label class="text  font-weight-bolder"> From Time: </label>
                            <vue-timepicker v-bind:key="randerTime" v-bind:disabled="address.allHour" @update:modelValue="CheckTimeGreater()" v-model="address.fromTime" input-width="100%" />
                        </div>
                        <div class="form-group has-label col-sm-4 ">
                            <label class="text  font-weight-bolder"> To Time: </label>
                            <vue-timepicker v-bind:key="randerTime" v-bind:disabled="address.allHour" @update:modelValue="CheckTimeGreater()" v-model="address.toTime" input-width="100%" />
                        </div>
                        <div class="form-group has-label col-sm-4 ">
                            <label class="text  font-weight-bolder"> Week Days: </label>
                            <multiselect v-bind:disabled="address.allDaySelection" v-model="processContractors" @update:modelValue="contractorListId(processContractors)" :options="options" :multiple="true" placeholder="Select Days" track-by="name" :clear-on-select="false" :show-labels="false" label="name" :preselect-first="true">
                            </multiselect>
                        </div>

                    </div>
                    <div class="row">
                        <b style="padding-bottom: 5px !important;">Other Contact Detail on a Location</b>
                        <div class="form-group has-label col-sm-6 ">
                            <label class="text  font-weight-bolder"> Coordinates (Longitude ): </label>

                            <input class="form-control" v-model="address.langitutue" @update:modelValue="GetLocation('Coordinates')" type="text" />
                        </div>

                        <div class="form-group has-label col-sm-6 ">
                            <label class="text  font-weight-bolder"> Coordinates (Latitude ): </label>

                            <input class="form-control" v-model="address.latitude" @update:modelValue="GetLocation('Coordinates')" type="text" />
                        </div>
                        <div class="form-group has-label col-sm-6 ">
                            <label class="text  font-weight-bolder"> Google Location: </label>
                            <input class="form-control" v-model="address.googleLocation" @update:modelValue="GetLocation('Location')" type="text" />
                        </div>
                        <div class="form-group has-label col-sm-6 ">
                            <label class="text  font-weight-bolder">Nearest Landmark: </label>
                            <input class="form-control" v-model="address.nearBy" type="text" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-2" v-if="address.isFirst">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox3" disabled v-model="address.isDefault">
                                <label for="inlineCheckbox3"> Default</label>
                            </div>
                        </div>
                        <div class="form-group col-md-2" v-else-if="type=='Edit' && address.isOrignal ">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox3" v-bind:disabled="address.isDefault" v-model="address.isDefault">
                                <label for="inlineCheckbox3"> Default</label>
                            </div>
                        </div>
                        <div class="form-group col-md-2" v-else-if="type=='Edit' && !address.isOrignal ">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox3" v-model="address.isDefault">
                                <label for="inlineCheckbox3"> Default</label>
                            </div>
                        </div>
                        <div class="form-group col-md-2" v-else>
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox3" v-model="address.isDefault">
                                <label for="inlineCheckbox3"> Default</label>
                            </div>
                        </div>
                        <div class="form-group col-md-2">
                            <div class="checkbox form-check-inline mx-2">
                                <input type="checkbox" id="inlineCheckbox14" v-model="address.isActive">
                                <label for="inlineCheckbox14"> {{ $t('AddColors.Active') }} </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer" v-if="isSale==true">
                <button type="button" class="btn btn-soft-primary btn-sm" v-bind:disabled="v$.address.$invalid" v-on:click="SaveColor" data-bs-toggle="offcanvas" ref="offcanvasRight" data-bs-target="#offcanvasRight" v-if="type!='Edit' && !isCancel">{{ $t('AddColors.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-bind:disabled="v$.address.$invalid" v-on:click="SaveColor" data-bs-toggle="offcanvas" ref="offcanvasRight" data-bs-target="#offcanvasRight" v-if="type=='Edit' && !isCancel">{{ $t('AddColors.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()" data-bs-toggle="offcanvas" ref="offcanvasRight" data-bs-target="#offcanvasRight" v-if="!isCancel">{{ $t('AddColors.btnClear') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-bind:disabled="v$.address.$invalid" v-on:click="SaveColor" v-if="type!='Edit' && isCancel">{{ $t('AddColors.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-bind:disabled="v$.address.$invalid" v-on:click="SaveColor" v-if="type=='Edit' && isCancel">{{ $t('AddColors.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()" v-if="isCancel">{{ $t('AddColors.btnClear') }}</button>
            </div>
            <div class="modal-footer" v-else>
                <button type="button" class="btn btn-soft-primary btn-sm" v-bind:disabled="v$.address.$invalid" v-on:click="SaveColor" v-if="type!='Edit'">{{ $t('AddColors.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-bind:disabled="v$.address.$invalid" v-on:click="SaveColor" v-if="type=='Edit'">{{ $t('AddColors.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddColors.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>

    </modal>
</template>

<script>
    import Multiselect from 'vue-multiselect'
    import VueTimepicker from 'vue3-timepicker/src/VueTimepicker.vue';
    import clickMixin from '@/Mixins/clickMixin'
   import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    // import moment from "moment";

    import {
        required
    } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        name: 'AddressDropDown',
        props: ['show', 'newaddress', 'type', 'isSale', 'list'],
        mixins: [clickMixin],
        components: {
            Multiselect,
            VueTimepicker,
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                processContractors: [],
                options: [],
                arabic: '',
                english: '',
                location: '',
                render: 0,
                randerTime: 0,
                loading: false,
                isCancel: false,
                address: {},
            }
        },
        validations() {
            return {
                address: {

                    address: {
                        required,
                    },
                    type: {
                        required,
                    },
                    area: {
                        required,
                    },
                    contactName: {
                        required,
                    },
                    contactNumber: {
                        required,
                    },

                }
            }
        },
        methods: {
            GetAddressFor: function () {

                if (!this.isSale) {
                    if (this.list != undefined && this.list != null) {
                        if (this.list.length != 0) {
                            var isDefault = this.list.some(x => x.type == this.address.type && x.isDefault);
                            if (!isDefault) {
                                this.address.isDefault = true;
                                this.address.isFirst = true;
                            }
                            else {
                                this.address.isFirst = false;
                                this.address.isDefault = false;

                            }
                        }

                    }


                }

            },

            GetLocation: function (type) {

                if (type == 'Coordinates') {
                    {

                        this.address.googleLocation = ' http://maps.google.com/?q=' + this.address.langitutue + ',' + this.address.latitude
                    }

                }
                if (type == 'Location') {
                    if (this.address.googleLocation != '') {

                        const url = this.address.googleLocation;
                        const regex = /[-+]?[0-9]*\.?[0-9]+/g; // regex to match any number (positive or negative) with or without a decimal point
                        const matches = url.match(regex); // get all matches in the url
                        this.address.langitutue = parseFloat(matches[0]); // parse the first match as latitude
                        this.address.latitude = parseFloat(matches[1]);
                    }

                }

            },
            CheckTimeGreater: function () {
                debugger; //eslint-disable-line

                // if (this.address.fromTime != null && this.address.fromTime != '' && this.address.fromTime != undefined) {
                //     if (this.address.toTime != null && this.address.toTime != '' && this.address.toTime != undefined) {
                //         if (this.address.toTime.split(':')[1] != 'mm') {
                //             var hourForToTime = parseInt(this.address.toTime.split(':')[0]);
                //             var hourForFromTime = parseInt(this.address.fromTime.split(':')[0]);
                //             var minForToTime = parseInt(this.address.toTime.split(':')[1]);
                //             var minForFromTime = parseInt(this.address.fromTime.split(':')[1]);
                //             if (hourForToTime <= hourForFromTime) {
                //                 if (minForToTime < minForFromTime) {
                //                     this.$swal({
                //                         title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                //                         text: "From Time alwaysa less than to Time ",
                //                         type: 'error',
                //                         icon: 'error',
                //                         showConfirmButton: false,
                //                         timer: 2000,
                //                         timerProgressBar: true,
                //                     });
                //                 } {
                //                     this.$swal({
                //                         title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Error!' : 'خطأ',
                //                         text: "From Time alwaysa less than to Time ",
                //                         type: 'error',
                //                         icon: 'error',
                //                         showConfirmButton: false,
                //                         timer: 2000,
                //                         timerProgressBar: true,
                //                     });
                //                 }

                //             }

                //         }

                //     }
                // }

            },
            GetTime: function () {

                if (!this.address.allHour) {
                    this.address.fromTime = '00' + ':' + '00';
                    this.address.toTime = '23' + ':' + '00';
                } else {
                    this.address.fromTime = '';
                    this.address.toTime = '';
                }
                this.randerTime++;

            },
            AllDaySelection: function () {

                if (!this.address.allDaySelection) {
                    this.processContractors = this.options;
                    this.address.deliveryHolidays = this.options;

                } else {
                    this.processContractors = '';
                    this.address.deliveryHolidays = [];
                }

            },
            contractorListId: function (value) {

                var root = this;
                root.address.deliveryHolidays = value;

            },
            getData: function (x, value) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Payroll/WeekDaysList', {
                    headers: {
                        "Authorization": `Bearer ${token}`
                    }
                }).then(function (response) {

                    if (response.data != null) {

                        response.data.results.forEach(function (result) {

                            root.options.push({
                                weekHolidayId: result.id,
                                name: result.name,
                                nameArabic: result.nameArabic,
                            })
                            if (x) {

                                if (value != undefined) {
                                    value.forEach(function (ids) {

                                        if (ids.weekHolidayId == result.id) {

                                            root.processContractors.push({
                                                weekHolidayId: result.id,
                                                name: result.name,
                                                nameArabic: result.nameArabic,
                                            });
                                            root.contractorListId(root.processContractors);
                                        }
                                    })

                                }
                            }

                        })
                    }

                    //if (response.data != null) {
                    //    response.data.results.forEach(function (cat) {

                    //        root.options.push({
                    //            id: cat.id,
                    //            name: cat.name,
                    //            nameArabic: cat.nameArabic,
                    //        })

                    //    })
                    //}
                });
            },
            close: function () {

                this.$emit('close');
            },

            SaveColor: function () {

                if (this.list != undefined && this.list != null) {
                    if (this.list.length != 0 && !this.isSale && !this.address.isOrignal) {
                        var isDefault = this.list.some(x => x.type == this.address.type && x.isDefault);
                        if (isDefault) {
                            this.address.isAlreadyDefault = true;

                        }

                    }

                }


                if (this.address.isFirst) {
                    if (!this.address.isDefault) {
                        this.address.isDefault = true
                    }
                }
                var root = this;

                if (this.address.isAlreadyDefault && this.address.isDefault) {

                    root.$swal({
                        title: 'Are you sure?',
                        text: 'Change Default Address',
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: 'Confirm',
                        closeOnConfirm: false,
                        closeOnCancel: false
                    }).then(function (result) {
                        if (result.isConfirmed) {
                            if (root.address.fromTime != null && root.address.fromTime != undefined && root.address.fromTime != '') {
                                if (root.address.fromTime.HH != undefined) {
                                    if (root.address.fromTime.HH != null) {
                                        root.address.fromTime = root.address.fromTime.HH + ':' + root.address.fromTime.mm;

                                    }

                                }
                            }
                            if (root.address.toTime != null && root.address.toTime != undefined && root.address.toTime != '') {
                                if (root.address.toTime.HH != undefined) {
                                    if (root.address.toTime.HH != null) {
                                        root.address.toTime = root.address.toTime.HH + ':' + root.address.toTime.mm;

                                    }

                                }
                            }

                            root.$emit('IsSave', root.address);

                        } else {
                            root.isCancel = true;
                            root.$swal('Cancelled', 'Please set Default unchecked',);
                        }
                    });
                } else {
                    if (root.address.fromTime != null && root.address.fromTime != undefined && root.address.fromTime != '') {
                        if (root.address.fromTime.HH != undefined) {
                            if (root.address.fromTime.HH != null) {
                                root.address.fromTime = root.address.fromTime.HH + ':' + root.address.fromTime.mm;

                            }

                        }
                    }
                    if (root.address.toTime != null && root.address.toTime != undefined && root.address.toTime != '') {
                        if (root.address.toTime.HH != undefined) {
                            if (root.address.toTime.HH != null) {
                                root.address.toTime = root.address.toTime.HH + ':' + root.address.toTime.mm;

                            }

                        }
                    }

                    root.$emit('IsSave', root.address);

                }

            }
        },
        created: function () {
             //eslint-disable-line
            this.address = this.newaddress;
        },
        mounted: function () {

             //eslint-disable-line

            if (!this.isSale) {
                if (this.list != undefined && this.list != null) {

                    if (this.list.length == 0) {
                        this.address.isDefault = true;
                        this.address.isFirst = true;

                    }

                }


            }

            if (this.address.add == 'Edit' || this.address.add == 'Duplicate') {

                this.getData(true, this.address.deliveryHolidays);
            } else {
                this.getData(false);

            }
            // this.newaddress1=this.address;

            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

        }
    }
</script>
