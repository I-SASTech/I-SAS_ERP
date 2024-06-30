<template>
    <div class="mt-3" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'">
        <div class="card">
            <div class="card-body">
                <div class="row">
                    <div class="col-lg-12 ">
                        <div class="row">
                            <div class="col-sm-6 col-md-6 col-lg-6">
                                <h5 class="card-title mt-3">{{newCustomer.customerDisplayName}}</h5>
                            </div>
                            <div class=" col-sm-6 col-md-6 col-lg-6">
                                <div v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                    <button v-on:click="EditCustomer(newCustomer.id)" class="btn btn-icon btn-twitter">
                                        <i class="fas fa-edit"></i>
                                    </button>
                                    <button v-on:click="Attachment()" class="btn btn-icon  btn-google">
                                        <i class="fas fa-paperclip"></i>
                                    </button>
                                    <button v-on:click="Cancel()" class="btn btn-icon  btn-google">
                                        <i class="fas fa-window-close"></i>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-12">
                        <ul class="nav nav-tabs nav-tabs-dashed" id="tabs" role="tablist">
                            <li class="active  nav-item"><a data-toggle="tab" class="active nav-link" href="#OverView" aria-expanded="true">Overview</a></li>
                            <li class=" nav-item"><a data-toggle="tab" class="nav-link" href="#link10" aria-expanded="false">Comments</a></li>
                            <div class="ml-auto"></div>
                            <div class="mr-3 align-self-center">
                                <div id="ember2435" class="popover-container ">
                                    <div id="ember2436" class="">
                                        <img class="cursor-pointer" src="https://d31g2a6snus4ly.cloudfront.net/zbooks/assets/images/cliq-aaa1ab5e9fe8d371616faa972693b8f9.png" height="16px"> <!---->
                                    </div>
                                </div>
                            </div>
                        </ul>

                        <div id="my-tab-content" class="tab-content">
                            <div class="tab-pane active" id="OverView" role="tabpanel" aria-expanded="true">
                                <div class="viewdetail">
                                    <div class="details fill-container noscroll-x">
                                        <div class="details-info">
                                            <div class="group">
                                                <div>{{newCustomer.companyNameEnglish}}</div>
                                                <hr>
                                                <div class="person row">
                                                    <div class="col-lg-2">
                                                        <img src="https://secure.gravatar.com/avatar/46e3aa95c98dfff17aebb060ae388e36?&amp;d=mm" class="cp-pic">
                                                    </div>
                                                    <div class="col-lg-10">
                                                        <strong>{{newCustomer.customerDisplayName}}</strong>
                                                        <br> {{newCustomer.email}}
                                                        <br> {{getType(newCustomer.supplierType)}}
                                                        <br><i class="fas fa-mobile-alt"></i> {{newCustomer.telephone}}
                                                        <br><i class="fas fa-mobile-alt"></i> {{newCustomer.contactNo1}}
                                                        <br>
                                                        <a v-if="newCustomer.skype!='' && newCustomer.skype!=null" style="color: #333;" href="" target="_blank" rel="noreferrer noopener">
                                                            <i class="fab fa-skype"></i> {{newCustomer.skype}}
                                                        </a>
                                                        <br>

                                                        <div class="line-seperator">
                                                            <small class="coll">
                                                                <a v-on:click="EditCustomer(newCustomer.id)" href="javascript:void(0)" data-ember-action="" data-ember-action-2441="2441">Edit</a>
                                                            </small>
                                                            <small class="coll">
                                                                <a href="javascript:void(0)" data-ember-action="" data-ember-action-2442="2442">Send Email</a>
                                                            </small>
                                                            <small class="coll">
                                                                <a href="javascript:void(0)" data-ember-action="" data-ember-action-2443="2443">  Reinvite  </a>
                                                            </small>
                                                            <small class="coll">
                                                                <a href="javascript:void(0)" data-ember-action="" data-ember-action-2444="2444">Delete</a>
                                                            </small>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div id="accordion" role="tablist" aria-multiselectable="true" class="card-collapse customer-view-pannel">
                                                    <div class="card card-plain">
                                                        <div class="card-header" role="tab" id="heading1">
                                                            <a data-toggle="collapse" data-parent="#accordion" href="#collapse1" aria-expanded="true" aria-controls="collapseOne" style="text-decoration: none;">
                                                                ADDRESS
                                                                <i class="nc-icon nc-minimal-down"></i>
                                                            </a>
                                                        </div>
                                                        <div id="collapse1" class="collapse show" role="tabpanel" aria-labelledby="heading1">
                                                            <div class="row mt-2">
                                                                <div class="col-lg-12">
                                                                    <div class="group">
                                                                        <div> Billing Address</div>
                                                                        <div>
                                                                            <address class="font-small">
                                                                                <strong v-if="newCustomer.billingAttention!='' && newCustomer.billingAttention!=null">{{newCustomer.billingAttention}}</strong>
                                                                                <div v-if="newCustomer.billingAddress!='' && newCustomer.billingAddress!=null" class="preserve-wrap">{{newCustomer.billingAddress}}</div>
                                                                                <span v-if="newCustomer.billingCity!='' && newCustomer.billingCity!=null">{{newCustomer.billingCity}}<br></span>
                                                                                <span v-if="newCustomer.billingArea!='' && newCustomer.billingArea!=null && newCustomer.billingZipCode!=null && newCustomer.billingZipCode!=null">{{newCustomer.billingArea}} {{newCustomer.billingZipCode}}<br></span>
                                                                                <span v-if="newCustomer.billingCountry!='' && newCustomer.billingCountry!=null">{{newCustomer.billingCountry}}<br></span>
                                                                                <span v-if="newCustomer.billingPhone!='' && newCustomer.billingPhone!=null">Phone: {{newCustomer.billingPhone}}<br></span>
                                                                                <span v-if="newCustomer.billingFax!='' && newCustomer.billingFax!=null">Fax: {{newCustomer.billingFax}}</span>
                                                                            </address>
                                                                        </div>

                                                                        <div> Shipping Address</div>
                                                                        <div>
                                                                            <address class="font-small">
                                                                                <strong v-if="newCustomer.shippingAttention!='' && newCustomer.shippingAttention!=null">{{newCustomer.shippingAttention}}</strong>
                                                                                <div v-if="newCustomer.shippingAddress!='' && newCustomer.shippingAddress!=null" class="preserve-wrap">{{newCustomer.shippingAddress}}</div>
                                                                                <span v-if="newCustomer.shippingCity!='' && newCustomer.shippingCity!=null">{{newCustomer.shippingCity}}<br></span>
                                                                                <span v-if="newCustomer.shippingArea!='' && newCustomer.shippingArea!=null && newCustomer.shippingZipCode!=null && newCustomer.shippingZipCode!=null">{{newCustomer.shippingArea}} {{newCustomer.shippingZipCode}}<br></span>
                                                                                <span v-if="newCustomer.shippingCountry!='' && newCustomer.shippingCountry!=null">{{newCustomer.shippingCountry}}<br></span>
                                                                                <span v-if="newCustomer.shippingPhone!='' && newCustomer.shippingPhone!=null">Fax: {{newCustomer.shippingPhone}}</span>
                                                                                <span v-if="newCustomer.shippingFax!='' && newCustomer.shippingFax!=null">Fax: {{newCustomer.shippingFax}}</span>
                                                                            </address>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card card-plain">
                                                        <div class="card-header" role="tab" id="heading2">
                                                            <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapse2" aria-expanded="true" aria-controls="collapse2" style="text-decoration: none;">
                                                                OTHER DETAILS
                                                                <i class="nc-icon nc-minimal-down"></i>
                                                            </a>
                                                        </div>
                                                        <div id="collapse2" class="collapse show" role="tabpanel" aria-labelledby="heading2">
                                                            <div class="row mt-2">
                                                                <div class="col-lg-12">
                                                                    <div id="ember4154" class="ember-view collapse show" style="">
                                                                        <div class="group">
                                                                            <div id="ember4155" class="edit-field ember-view">
                                                                                <div class="row inline-fields">
                                                                                    <label class="col-lg-5 text-muted"> Customer Type </label>
                                                                                    <div class="col-lg-7 edit-form-field">
                                                                                        {{newCustomer.category}}
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div id="ember4159" class="edit-field ember-view">
                                                                                <div class="row inline-fields">
                                                                                    <label class="col-lg-5 text-muted"> Default Currency </label>
                                                                                    <div class="col-lg-7 edit-form-field">
                                                                                        {{newCustomer.currencyName}}
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row inline-fields">
                                                                                <label class="col-lg-5 text-muted"> Payment Terms </label>
                                                                                <div class="col-lg-7"> Due on Receipt </div>
                                                                            </div>
                                                                            <div id="ember4163" class="edit-field ember-view">
                                                                                <div class="row inline-fields">
                                                                                    <label class="col-lg-5 text-muted"> Website </label>
                                                                                    <div class="col-lg-7 edit-form-field">
                                                                                        <a target="_blank" rel="noopener noreferrer" :href="newCustomer.website">{{newCustomer.website}}</a>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="row inline-fields">
                                                                                <label class="col-lg-5 text-muted"> Tax Rate </label>
                                                                                <div class="col-lg-7">{{newCustomer.taxRateName}} </div>
                                                                            </div>
                                                                            <div class="row inline-fields">
                                                                                <label class="col-lg-5 text-muted">Social Networks</label>
                                                                                <div class="col-lg-7">
                                                                                    <a style="margin-right: 10px;font-size: 18px;" :href="'https://www.facebook.com/'" target="_blank" rel="noreferrer noopener">
                                                                                        <i id="ember4172" class="tooltip-container ember-view">
                                                                                            <i class="fab fa-facebook-square"></i>
                                                                                        </i>
                                                                                    </a>
                                                                                    <a style="font-size: 18px;" :href="'https://twitter.com/'" target="_blank" rel="noreferrer noopener">
                                                                                        <i id="ember4173" class="tooltip-container ember-view">
                                                                                            <i class="fab fa-twitter-square"></i>
                                                                                        </i>
                                                                                    </a>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="card card-plain">
                                                        <div class="card-header" role="tab" id="heading3">
                                                            <a class="collapsed" data-toggle="collapse" data-parent="#accordion" href="#collapse3" aria-expanded="false" aria-controls="collapse3" style="text-decoration: none;">
                                                                CONTACT PERSONS
                                                                <i class="nc-icon nc-minimal-down"></i>
                                                            </a>
                                                        </div>
                                                        <div id="collapse3" class="collapse" role="tabpanel" aria-labelledby="heading3">
                                                            <div class="row mt-2">
                                                                <div class="col-lg-12">
                                                                    <div v-for="(person, index) in newCustomer.contactPersonList" :key="index" class="row mb-3" >
                                                                        <div class="col-lg-2">
                                                                            <img src="/images/avatar-placeholder.png" class="cp-pic">
                                                                        </div>
                                                                        <div class="col-lg-10 font-small">
                                                                            <strong>{{person.prefix}} {{person.firstName}} {{person.lastName}} </strong>
                                                                            <span v-if="person.email!='' && person.email!=null"><br> {{person.email}}</span>
                                                                            <span v-if="person.phone!='' && person.phone!=null"><br><i class="fas fa-mobile-alt"></i> {{person.phone}}</span>
                                                                            <span v-if="person.mobile!='' && person.mobile!=null"><br><i class="fas fa-mobile-alt"></i> {{person.mobile}}</span>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="entity-details">

                                        </div>
                                    </div>
                                </div>


                            </div>
                            <div class="tab-pane" id="link10" role="tabpanel" aria-expanded="false">
                                <div class="row p-5">
                                    <div class="col-lg-8">
                                        <div class="row form-group">
                                            <div class="col-lg-12">
                                                <label class="col-form-label">Remarks (For Internal Use)</label>
                                                <textarea v-model="newCustomer.comments" class="form-control"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
            <bulk-attachment :attachmentList="newCustomer.attachmentList" :show="show" v-if="show" @close="attachmentSave" />
        </div>
    </div>


</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import moment from 'moment'
    import { required, maxLength, requiredIf } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
    

    export default ({
        mixins: [clickMixin],
        components: {
            Loading
        },
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                arabic: '',
                show: false,
                b2b: false,
                b2c: false,
                english: '',

                isSkype: false,
                salutatioRender: 0,

                newCustomer: {
                    id: '00000000-0000-0000-0000-000000000000',
                    code: '',

                    prefix: '',
                    englishName: '',
                    arabicName: '',
                    companyNameEnglish: '',
                    companyNameArabic: '',
                    customerDisplayName: '',
                    telephone: '',
                    email: '',
                    registrationDate: '',
                    category: '',
                    customerType: '',
                    customerGroup: '',
                    contactNo1: '',

                    commercialRegistrationNo: '',
                    vatNo: '',

                    currencyId: '',
                    taxRateId: '',
                    website: '',

                    billingAttention: '',
                    billingCountry: '',
                    billingZipCode: '',
                    billingPhone: '',
                    billingArea: '',
                    billingAddress: '',
                    billingCity: '',
                    billingFax: '',

                    shippingAttention: '',
                    shippingCountry: '',
                    shippingZipCode: '',
                    shippingPhone: '',
                    shippingArea: '',
                    shippingAddress: '',
                    shippingCity: '',
                    shippingFax: '',

                    contactPersonList: [{ prefix: '', firstName: '', lastName: '', email: '', phone: '', mobile: '' }],

                    remarks: '',
                    comments: '',
                    isCustomer: true,
                    isActive: true,

                    paymentTerms: '',
                    deliveryTerm: '',
                    creditLimit: '',
                    creditPeriod: '',

                    attachmentList: []
                },
                loading: false,
                language: 'Nothing',
            }
        },
        validations() {
            return {
                newCustomer:
                {
                    customerType: { required },
                    englishName: {
                        required: requiredIf((x) => {
                            if (x.arabicName == '' || x.arabicName == null)
                                return true;
                            return false;
                        }),
                        maxLength: maxLength(250)
                    },
                    arabicName: {
                        maxLength: maxLength(250)
                    },
                    vatNo: {
                        required: requiredIf((x) => {
                            if (x.commercialRegistrationNo == '' || x.commercialRegistrationNo == null)
                                return true;
                            return false;
                        }),
                        maxLength: maxLength(250)
                    },
                    paymentTerms: { required }
                }
                }
        },

        methods: {
            getType: function (x) {
                this.language = this.$i18n.locale;

                if (this.language == 'en') {
                    if (x == 1) {
                        return 'Wholesaler';
                    }
                    else if (x == 2) {
                        return 'Retailer';
                    }
                    else if (x == 5) {
                        return 'Wholesaler & Retailer';
                    }
                    else if (x == 3) {
                        return 'Dealer';
                    }
                    else if (x == 4) {
                        return 'Distributor';
                    }
                    else {
                        return '';
                    }
                }
                else {
                    if (x == 1) {
                        return 'جمله';
                    }
                    else if (x == 2) {
                        return 'قطاعي';
                    }
                    else if (x == 5) {
                        return 'بائع بالجملة';
                    }
                    else if (x == 3) {
                        return 'وكيل';
                    }
                    else if (x == 4) {
                        return 'موزع';
                    }
                    else {
                        return '';
                    }
                }

            },

            Attachment: function () {
                this.show = true;
            },

            attachmentSave: function (attachment) {
                this.newCustomer.attachmentList = attachment;
                this.show = false;
            },

            languageChange: function (lan) {
                if (this.language == lan) {
                    if (this.newCustomer.id == '00000000-0000-0000-0000-000000000000') {

                        var getLocale = this.$i18n.locale;
                        this.language = getLocale;

                        this.$router.go('/addCustomer');
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

            EditCustomer: function (Id) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Contact/ContactDetail?Id=' + Id, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {
                        root.customer = response.data
                        root.$store.GetEditObj(root.customer);
                    }

                    root.$router.push({
                        path: '/addsupplier',
                        query: {
                            data: '',
                            Add: false,
                        }
                    })
                });
            },


            AddRow: function () {
                this.newCustomer.contactPersonList.push({ prefix: '', firstName: '', lastName: '', email: '', phone: '', mobile: '' });
            },
            RemoveRow: function (index) {
                this.newCustomer.contactPersonList.splice(index, 1);
            },

            Cancel: function () {
                this.$router.push({
                    path: '/supplier',

                })
            },

        },
        created: function () {

            this.$emit('update:modelValue', this.$route.name)
            if (this.$route.query.data != undefined) {
                this.newCustomer = this.$route.query.data;
            }
            else {
                this.GetAutoCodeGenerator();
                this.newCustomer.registrationDate = moment().format('llll');
                if ((this.$i18n.locale == 'en' || this.isLeftToRight())) {
                    this.newCustomer.paymentTerms = 'Cash';
                }
                else {
                    this.newCustomer.paymentTerms = 'آجل';
                }

                this.b2b = localStorage.getItem('b2b') == 'true' ? true : false;
                this.b2c = localStorage.getItem('b2c') == 'true' ? true : false;
                if (this.b2b && !this.b2c) {
                    this.newCustomer.category = 'B2B – Business to Business';
                }
                if (!this.b2c && this.b2c) {
                    this.newCustomer.category = 'B2C – Business to Client';
                }
            }
        },
        mounted: function () {
            this.language = this.$i18n.locale;
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
        }
    })

</script>