
<template>
    <modal :show="show" v-if=" isValid('CanAddTerminal') || isValid('CanEditTerminal') || isValid('Noble Admin')">

        <div class="modal-content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header" v-if="type=='Edit'">

                            <h6 class="modal-title m-0" id="myModalLabel">      {{ $t('AddTerminal.UpdateTerminal') }}</h6>
                            <button type="button" class="btn-close" v-on:click="close()"></button>
                        </div>
                        <div class="modal-header" v-else>
                            <h6 class="modal-title DayHeading" id="myModalLabel">{{ $t('AddTerminal.AddTerminal') }}</h6>
                            <button type="button" class="btn-close" v-on:click="close()"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row ">

                                <div :key="render" class="form-group has-label col-sm-6 " v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')">
                                    <label class="text  font-weight-bolder"> {{ $t('AddTerminal.TerminalType') }}:<span class="text-danger"> *</span></label>
                                    <multiselect v-model="terminal.terminalType" v-if="($i18n.locale == 'en' ||isLeftToRight()) " @update:modelValue="GetAutoCodeGenerator" :options="[ 'Terminal', 'CashCounter']" :show-labels="false" placeholder="Select Type">
                                    </multiselect>
                                    <multiselect v-else v-model="terminal.terminalType" :options="[ 'شباك النقود', 'صالة']" @change="GetAutoCodeGenerator" :show-labels="false" v-bind:placeholder="$t('AddTerminal.SelectOption')">
                                    </multiselect>
                                </div>
                                <div :key="render1" class="form-group has-label col-sm-6 " v-else>
                                    <label class="text  font-weight-bolder"> {{ $t('AddTerminal.TerminalType') }}:<span class="text-danger"> *</span></label>
                                    <multiselect v-model="terminal.terminalType" v-if="($i18n.locale == 'en' ||isLeftToRight()) " @input="GetAutoCodeGenerator" :options="[ 'Terminal']" :show-labels="false" placeholder="Select Type">
                                    </multiselect>
                                    <multiselect v-else v-model="terminal.terminalType" :options="['صالة']" @change="GetAutoCodeGenerator" :show-labels="false" v-bind:placeholder="$t('AddTerminal.SelectOption')">
                                    </multiselect>
                                </div>
                                <div :key="renderCode" class="form-group has-label col-sm-6 ">
                                    <label class="text  font-weight-bolder"> {{ $t('AddTerminal.Code') }}:<span class="text-danger"> *</span></label>
                                    <input disabled class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'" v-model="v$.terminal.code.$model" type="text" />
                                    <span v-if="v$.terminal.code.$error" class="error">
                                        <span v-if="!v$.terminal.code.maxLength">{{ $t('AddTerminal.CodeLength') }}</span>
                                    </span>
                                </div>


                                <div class="form-group has-label col-sm-6 " v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')">
                                    <label class="text  font-weight-bolder">   {{$t('AddTerminal.BankAccount')}}:<span class="text-danger"> *</span> </label>
                                    <accountdropdown @update:modelValue="getbankPosTerminals(terminal.accountId)" v-model="terminal.accountId" :formName="bank" :companyId="terminal.companyId"></accountdropdown>

                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')">
                                    <label class="text  font-weight-bolder">   {{ $t('AddTerminal.BankPosTerminal') }} : </label>
                                    <!--<bankPosTerminalDropdown  v-model="terminal.posTerminalId" :formName='cash'></bankPosTerminalDropdown>-->

                                    <div v-bind:style="($i18n.locale == 'en' ||isLeftToRight()) ? '' : 'padding-top:5px;'">
                                        <multiselect v-model="posTerminalId" @update:modelValue="OnSelectedValue(posTerminalId.id)" :options="bankposTerminalOptions" :disabled="terminal.accountId=='' || terminal.accountId==null" :show-labels="false" track-by="terminalId" label="terminalId" v-bind:placeholder="$t('AddTerminal.PleaseSelectBankAccount')">
                                        </multiselect>
                                    </div>
                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="terminal.companyId != ''">
                                    <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddTerminal.CompanyName(InEnglish)'))  }}:</label>
                                    <input class="form-control" v-bind:disabled="!terminal.overWrite" v-model="terminal.companyNameEnglish" type="text" />

                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="terminal.companyId != ''">
                                    <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddTerminal.CompanyName(InEnglish)'))   }}:</label>
                                    <input class="form-control" v-bind:disabled="!terminal.overWrite" v-model="terminal.companyNameArabic" type="text" />

                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="terminal.companyId != ''">
                                    <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddTerminal.BusinessNameInEnglish'))  }}:</label>
                                    <input class="form-control" v-bind:disabled="!terminal.overWrite" v-model="terminal.businessNameEnglish" type="text" />

                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="terminal.companyId != ''">
                                    <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddTerminal.BusinessNameInEnglish'))  }}:</label>
                                    <input class="form-control" v-bind:disabled="!terminal.overWrite" v-model="terminal.businessNameArabic" type="text" />

                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="terminal.companyId != ''">
                                    <label class="text  font-weight-bolder"> {{$englishLanguage($t('AddTerminal.BusinessType(InEnglish)'))  }}:</label>
                                    <input class="form-control" v-bind:disabled="!terminal.overWrite" v-model="terminal.businessTypeEnglish" type="text" />

                                </div>
                                <div class="form-group has-label col-sm-6 " v-if="terminal.companyId != ''">
                                    <label class="text  font-weight-bolder"> {{$arabicLanguage($t('AddTerminal.BusinessType(InEnglish)'))  }}:</label>
                                    <input class="form-control" v-bind:disabled="!terminal.overWrite" v-model="terminal.businessTypeArabic" type="text" />

                                </div>
                                <div class="form-group has-label col-xm-12  col-md-6">
                                    <label>Terminal User Type :<span class="text-danger"> </span></label>

                                    <multiselect :disabled="terminalUserTypeDisabled" :options="terminalUserTypeOptions"
                                                 v-model="terminal.terminalUserType"
                                                 :show-labels="false" placeholder="Terminal User Type">
                                    </multiselect>
                                </div>
                                <div class="form-group has-label col-xm-12  col-md-6" v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')">
                                    <label class="text  font-weight-bolder" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'text-right'"> {{ $t('AddTerminal.Printer') }}: <span class="text-danger"> *</span></label>
                                    <printer-list-dropdown v-model="terminal.printerName" v-bind:key="renderPrinter" :modelValue="terminal.printerName"></printer-list-dropdown>
                                </div>

                                <div class="form-group has-label col-sm-12" v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')">
                                    <label>{{ $t('AddTerminal.ProductCategory') }} :<span class="text-danger"> *</span></label>
                                    <multiselect v-model="categoryId" :options="categoryOptions" :show-labels="false" track-by="name" :multiple="true" label="name" v-bind:placeholder="$t('AddTerminal.PleaseSelectProductCategory')">
                                        
                                        <template v-slot:noResult>
                <p  class="text-danger"> Oops! No Item found.</p>
            </template>
                                    </multiselect>
                                </div>

                                <div class="form-group has-label col-md-6" v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')">

                                    <div class="checkbox form-check-inline mx-2">
                                        <input type="checkbox" id="inlineCheckbox1" v-model="terminal.onPageLoadItem">
                                        <label for="inlineCheckbox1"> {{ $t('AddTerminal.OnPageLoadItem') }}  :</label>
                                    </div>

                                </div>
                                <div class="form-group has-label  col-md-6" v-if="isValid('TouchInvoiceTemplate1') || isValid('CanStartDay')">
                                    <div class="checkbox form-check-inline mx-2">
                                        <input type="checkbox" id="inlineCheckbox2" v-model="allowAll" v-on:change="SelectAllCategory(allowAll)">
                                        <label for="inlineCheckbox2">{{ $t('AddTerminal.AllowAll') }}:</label>
                                    </div>
                                </div>
                                <div class="form-group col-xm-12  col-md-6" v-if=" terminal.companyId != ''">
                                    <div class="checkbox form-check-inline mx-2">
                                        <input type="checkbox" id="inlineCheckbox3" v-model="terminal.overWrite">
                                        <label for="inlineCheckbox3">{{ $t('AddTerminal.OverWrite') }}:</label>
                                    </div>

                                </div>
                                <div class="form-group has-label col-xm-12  col-md-12">
                                    <button type="button" class="btn btn-soft-primary  " v-on:click="GetMacAddress" > Get Mac Address</button>
                            
                                </div>


                                <div class="col-sm-12" v-if=" terminal.companyId != ''">
                                    <div :key="renderImg">
                                        <input ref="imgupload" type="file"
                                               id="file-input"
                                               @change="uploadImage()"
                                               accept="image/*"
                                               name="image"
                                               v-if="!((imageSrc == '' && terminal.businessLogo!='') || (imageSrc != '' && terminal.businessLogo=='') || (imageSrc != '' && terminal.businessLogo!=''))"
                                               style="opacity:1;padding:25px">

                                        <div class="mt-3" v-if="imageSrc != ''">
                                            <img v-if="imageSrc != ''" :src="imageSrc" width="100" />
                                        </div>
                                        <div v-else class="mt-3">
                                            <span v-if="terminal.businessLogo!=null && terminal.businessLogo!=''">
                                                <!--<productimage v-bind:path="terminal.businessLogo" />-->
                                                <img :src="'data:image/png;base64,' + terminal.businessLogo" width="100" />
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12" v-if="terminal.companyId != ''">
                                    <button class="btn btn-danger  btn-sm" v-if="imageSrc != '' || terminal.businessLogo!=''" v-on:click="removeImage()">Remove</button>
                                </div>

                            </div>
                        </div>

                        <div class="modal-footer " v-if="type=='Edit' && (isValid('CanEditTerminal')|| isValid('Noble Admin'))">

                            <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveTerminal" v-bind:disabled="v$.terminal.$invalid"> {{ $t('AddTerminal.btnUpdate') }}</button>
                            <button type="button" class="btn btn-danger btn-sm" v-on:click="close()">{{ $t('AddTerminal.btnClear') }}</button>

                        </div>
                        <div class="modal-footer justify-content-right" v-if="type!='Edit' && (isValid('CanAddTerminal')|| isValid('Noble Admin'))">

                            <button type="button" class="btn btn-soft-primary btn-sm  " v-on:click="SaveTerminal" v-bind:disabled="v$.terminal.$invalid"> {{ $t('AddTerminal.btnSave') }}</button>
                            <button type="button" class="btn btn-soft-secondary btn-sm " v-on:click="close()">{{ $t('AddTerminal.btnClear') }}</button>

                        </div>
                    </div>
                </div>
            </div>
            <loading v-model:active="loading" :can-cancel="true" :is-full-page="true"></loading>
        </div>

    </modal>
    <acessdenied v-else :model=true></acessdenied>
</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'

    import Multiselect from 'vue-multiselect'
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        mixins: [clickMixin],
        components: {
            Multiselect,
            Loading
        },
        props: ['show', 'newterminal', 'type'],
        setup() {
            return { v$: useVuelidate() }
        },
        data: function () {
            return {
                allowAll: false,
                modalLarge: true,
                render: 0,
                render1: 0,
                renderCode: 0,
                renderPrinter: 0,
                cash: 'CashReceipt',
                bank: 'BankReceipt',
                bankposTerminalOptions: [],
                categoryOptions: [],
                categoryId: [],
                posTerminalId: '',
                overWrite: false,
                renderImg: 0,
                imageSrc: '',
                terminalUserTypeOptions: [],
                terminalUserTypeDisabled: false,
                terminal: {},
            }
        },
        validations() {
            return {
                terminal: {
                    code: {
                        maxLength: maxLength(10),
                        required
                    },
                    accountId: {
                        required
                    },
                    terminalUserType: {
                        required
                    },
                    macAddress: {
                        required
                    },
                }
            }
        },
        methods: {
            GetMacAddress: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/GetMacAddress', { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.terminal.macAddress = response.data.macAddress;
                        root.terminal.ipAddress = response.data.ipAddress;

                    }
                });
            },
            removeImage: function () {
                this.imageSrc = '';
                this.terminal.businessLogo = '';
                this.renderImg++;

            },
            uploadImage: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                var file = this.$refs.imgupload.files;

                var fileData = new FormData();

                for (var k = 0; k < file.length; k++) {
                    fileData.append("files", file[k]);
                }

                this.imageSrc = URL.createObjectURL(this.$refs.imgupload.files[0]);

                root.$https.post('/Company/UploadFilesAsync', fileData, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data != null) {

                            root.terminal.businessLogo = response.data;
                        }
                    },
                        function () {
                            this.loading = false;
                            root.$swal({
                                title: root.$t('AddTerminal.Error'),
                                text: root.$t('AddTerminal.SomethingWrong'),
                                type: 'error',
                                confirmButtonClass: "btn btn-danger",
                                buttonsStyling: false
                            });
                        });
            },
            OnSelectedValue: function (id) {

                this.terminal.posTerminalId = id;
            },
            getbankPosTerminals: function (x) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                if(x == null){
                    x="";
                }

                this.posTerminalId = [];
                this.bankposTerminalOptions = [];
                this.$https.get('/Company/BankPosTerminalList?isActive=true' + '&bankId=' + x, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data != null) {

                        response.data.bankPosTerminals.forEach(function (result) {
                            if (result.id == root.terminal.posTerminalId) {
                                root.posTerminalId.push({
                                    id: result.id,
                                    terminalId: result.terminalId
                                })
                            }
                            root.bankposTerminalOptions.push({
                                id: result.id,
                                terminalId: result.terminalId
                            })
                        })
                    }
                })
            },
            close: function () {
                this.$emit('close');
            },
            GetAutoCodeGenerator: function () {

                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.$https.get('/Company/TerminalCode?terminalType=' + root.terminal.terminalType, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {
                        root.terminal.code = response.data;
                        root.render++;
                        root.render1++;
                        root.renderCode++;
                        root.renderPrinter++;
                    }
                });
            },
            SaveTerminal: function () {
                this.loading = true;
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                if (this.categoryId != null && this.categoryId != undefined && this.categoryId != '') {
                    this.terminal.categoryId = this.getCategoryId(this.categoryId);
                }
                //if (this.terminal.overWrite) {
                //    localStorage.setItem('BusinessLogo', this.terminal.businessLogo);
                //    localStorage.setItem('overWrite', this.terminal.overWrite);
                //    localStorage.setItem('BusinessNameArabic', this.terminal.businessNameArabic);
                //    localStorage.setItem('BusinessNameEnglish', this.terminal.businessNameEnglish);
                //    localStorage.setItem('BusinessTypeArabic', this.terminal.businessTypeArabic);
                //    localStorage.setItem('BusinessTypeEnglish', this.terminal.businessTypeEnglish);
                //    localStorage.setItem('CompanyNameArabic', this.terminal.companyNameArabic);
                //    localStorage.setItem('CompanyNameEnglish', this.terminal.companyNameEnglish);
                //}


                this.$https.post('/Company/SaveTerminal', this.terminal, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data.isSuccess == true) {

                        if (root.type != "Edit") {
                            root.$store.isterminalList.push({
                                id: response.data.terminals.id,
                                macAddress: response.data.terminals.macAddress,
                                ipAddress: response.data.terminals.ipAddress,
                                accountId: response.data.terminals.accountId,
                                posTerminalId: response.data.terminals.posTerminalId,
                                code: response.data.terminals.code,
                                printerName: response.data.terminals.printerName,
                                onPageLoadItem: response.data.terminals.onPageLoadItem,
                            })
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                text: "Your Terminal " + response.data.terminals.code + " has been created!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                        else {
                            var data = root.$store.isterminalList.find(function (x) {
                                return x.id == response.data.terminals.id;
                            });
                            data.id = response.data.terminals.id;
                            data.ipAddress = response.data.terminals.ipAddress;
                            data.macAddress = response.data.terminals.macAddress;
                            data.code = response.data.terminals.code;
                            data.accountId = response.data.terminals.accountId;
                            data.posTerminalId = response.data.terminals.posTerminalId;
                            data.printerName = response.data.terminals.printerName;
                            data.onPageLoadItem = response.data.terminals.onPageLoadItem;
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                text: "Your Terminal " + response.data.terminals.code + " has been updated!",
                                type: 'success',
                                icon: 'success',
                                showConfirmButton: false,
                                timer: 800,
                                timerProgressBar: true,
                            });
                            root.close();
                        }
                    }
                    else {
                        root.$swal({
                            title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                            text: "Your terminal Name  Already Exist!",
                            type: 'error',
                            icon: 'success',
                            showConfirmButton: false,
                            timer: 800,
                            timerProgressBar: true,
                        });
                    }
                });
            },

            SelectAllCategory: function () {
                this.getCategoryData(false);
                if (!this.allowAll) {
                    this.categoryId = [];
                }
            },

            getCategoryId: function (value) {
                var categoryId = [];
                for (var i = 0; i < value.length; i++) {
                    categoryId[i] = value[i].id
                }
                return categoryId;
            },

            getCategoryData: function (edit) {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.categoryOptions = [];
                this.$https.get('/Product/GetCategoryInformation?isActive=true' + '&companyId=' + this.terminal.companyId, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {

                    if (response.data != null) {

                        response.data.results.categories.forEach(function (cat) {
                            root.categoryOptions.push({
                                id: cat.id,
                                dropDownName: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != "" ? cat.code + ' ' + cat.name : cat.code + ' ' + cat.nameArabic) : (cat.nameArabic != "" ? cat.code + ' ' + cat.nameArabic : cat.code + ' ' + cat.name),
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != "" ? cat.name : cat.nameArabic) : (cat.nameArabic != "" ? cat.nameArabic : cat.name)

                            })
                        });

                        if (edit) {
                            root.terminal.categoryIdList.forEach(function (cat) {
                                var category = root.categoryOptions.find(x => x.id == cat);
                                if (category != undefined) {
                                    root.categoryId.push(category);
                                }
                            });
                        }

                        if (root.allowAll) {
                            root.categoryId = [];
                            response.data.results.categories.forEach(function (cat) {
                                root.categoryId.push({
                                    id: cat.id,
                                    dropDownName: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != "" ? cat.code + ' ' + cat.name : cat.code + ' ' + cat.nameArabic) : (cat.nameArabic != "" ? cat.code + ' ' + cat.nameArabic : cat.code + ' ' + cat.name),
                                    name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (cat.name != "" ? cat.name : cat.nameArabic) : (cat.nameArabic != "" ? cat.nameArabic : cat.name)

                                })
                            });
                        }

                    }
                });
            },
        },
        created: function () {
            this.terminal= this.newterminal;
        },
        mounted: function () {
            if (localStorage.getItem('TerminalUserType') == 'Online') {
                this.terminalUserTypeOptions.push('Online')
                this.terminal.terminalUserType = 'Online'
                this.terminalUserTypeDisabled = true
            }
            else if (localStorage.getItem('TerminalUserType') == 'Offline') {
                this.terminalUserTypeOptions.push('Offline')
                this.terminalUserTypeDisabled = true
                this.terminal.terminalUserType = 'Offline'
            }
            else {
                this.terminalUserTypeOptions.push('Online')
                this.terminalUserTypeOptions.push('Offline')
                /*this.terminalUserTypeOptions.push('Both')*/
                this.terminalUserTypeDisabled = false
            }
            if (this.terminal.id != '00000000-0000-0000-0000-000000000000' || this.terminal.id != undefined || this.terminal.id != '') {
                this.getbankPosTerminals(this.terminal.accountId);
            }

            if (this.terminal.id == '00000000-0000-0000-0000-000000000000') {
                this.getCategoryData(false);
            }
            else {
                this.getCategoryData(true);
            }
            this.renderImg++;

            if((!this.isValid('TouchInvoiceTemplate1') || !this.isValid('CanStartDay')) && this.terminal.id == '00000000-0000-0000-0000-000000000000')
            {
                if(this.$i18n.locale == 'en' || this.isLeftToRight())
                {
                    this.terminal.terminalType = "Terminal";
                }
                else
                {
                    this.terminal.terminalType = "صالة";
                }

                this.GetAutoCodeGenerator();
            }

        }
    }
</script>
