<template>
    <modal :show="show">

        <div style="margin-bottom:0px" class="card" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
            <div class="card-body">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">

                            <h5 class="modal-title DayHeading" id="myModalLabel"> {{ $t('UpdateInquiryStatus.UpdateStatus') }}</h5>

                        </div>
                        <div class="">
                            <div class="card-body" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                <div class="row ">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'">
                                        <label>{{ $t('UpdateInquiryStatus.Status') }}: <span class="text-danger"> *</span></label>

                                        <div>
                                            <multiselect :options="options" v-model="DisplayValue" :show-labels="false" track-by="name" :clear-on-select="false" label="name" :placeholder="$t('UpdateInquiryStatus.SelectStatus') " @search-change="addStatus">
                                                
                                                <template v-slot:noResult>
                                                    <span  class="btn btn-primary " v-on:click="AddNewStatus">Create New</span><br />
                                                </template>
                                            </multiselect>

                                        </div>
                                    </div>

                                    <div class="form-group has-label col-sm-12 ">
                                        <label class="text  font-weight-bolder"> {{ $t('UpdateInquiryStatus.ReasonDescription') }}: </label>
                                        <textarea class="form-control" v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-left' : 'arabicLanguage'" v-model="updateStatus.reason" type="text" />

                                    </div>



                                </div>
                            </div>
                        </div>
                        <div v-if="!loading">
                            <div class="modal-footer " v-bind:class="($i18n.locale == 'en' ||isLeftToRight()) ? 'text-right' : 'text-left'">
                                <button type="button" class="btn btn-primary  " v-on:click="UpdateStatus"> {{ $t('UpdateInquiryStatus.Save') }}</button>

                                <button type="button" class="btn btn-danger  mr-3 " v-on:click="close()">{{ $t('UpdateInquiryStatus.Close') }}</button>
                            </div>

                        </div>

                        <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>

                    </div>
                </div>
            </div>

        </div>
    </modal>

</template>
<script>
    import clickMixin from '@/Mixins/clickMixin'
    import Multiselect from 'vue-multiselect'
    
    

    export default {
        mixins: [clickMixin],
        props: ['show', 'inquiryId', 'status', 'values'],
        components: {
            
            Multiselect
        },
        data: function () {
            return {
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                options: [],
                value: '',

                updateStatus: {
                    inquiryStatusDynamicId: '',
                    inquiryId: '00000000-0000-0000-0000-000000000000'
                }
            }
        },
        validations() {

        },
        computed: {
            DisplayValue: {
                get: function () {
                    if (this.value != "" || this.value != undefined) {
                        return this.value;
                    }
                    return this.values;
                },
                set: function (value) {
                    this.value = value;
                    this.updateStatus.inquiryStatusDynamicId = this.value.id
                }
            },

        },

        methods: {
            addStatus: function (data) {
                if (data != '' && data != null && data != undefined) {
                    this.newStatus = data
                }
            },
            AddNewStatus: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get("/Project/SaveStatus?name=" + this.newStatus, { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            root.options.push({
                                id: response.data.id,
                                name: response.data.name
                            })
                            root.value = response.data
                            //root.inquiry.mediaType = response.data.name
                            root.inquiry.inquiryStatusId = response.data.id
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                    });


                //this.inquiry.mediaType = this.newMediaType
            },
            close: function () {
                this.$emit('close');
            },

            GetStatus: function () {
                var root = this;
                var token = "";
                if (token == '') {
                    token = localStorage.getItem("token");
                }

                root.$https.get("/Project/GetStatus", { headers: { Authorization: `Bearer ${token}` }, })
                    .then(function (response) {
                        if (response.data != null) {
                            response.data.forEach(function (x) {
                                root.options.push({
                                    id: x.id,
                                    name: x.name
                                })
                            })
                        }
                    })
                    .then(function () {
                        if (root.inquiry.inquiryStatusId != '00000000-0000-0000-0000-000000000000' && root.inquiry.inquiryStatusId != null) {
                            root.value = root.options.find(function (x) {
                                return x.id == root.values;
                            })
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Cannot Generate Auto Inoice Number!' : 'استوردلا يمكن إنشاء رقم فاتورة تلقائي!',
                                text: error.response.data,
                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });
                    });
            },
            UpdateStatus: function () {
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }

                root.$https.post('/Project/UpdateInquiryStatus', this.updateStatus, { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    if (response.data) {
                        root.$swal({
                            title: root.$t('SavedSuccessfully'),
                            text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Status Changed Successfully' : 'تم تغيير الحالة بنجاح',
                            type: 'success',
                            confirmButtonClass: "btn btn-success",
                            buttonStyling: false,
                            icon: 'success',
                            timer: 1500,
                            timerProgressBar: true,

                        })
                        root.$emit('update:modelValue', response.data)
                        root.loading = false;
                    }
                    root.loading = false;
                });
            },

        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            this.updateStatus.inquiryId = this.inquiryId
            this.GetStatus()
            //this.updateStatus.inquiryStatusDynamicId = this.inquiryStatusDynamicId

        }
    }</script>