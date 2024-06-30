
<template>
    <modal :show="show">
        <div class="modal-content">
            <div class="row">
                <div class="col-lg-12">
                    <div class="tab-content" id="nav-tabContent">
                        <div class="modal-header">
                            <h6 class="modal-title m-0" id="myModalLabel">Hold Setup</h6>
                            <button type="button" class="btn-close" v-on:click="close()"></button>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="inline-fields col-md-12 mb-2">
                                    <multiselect
                                        :options="['1 Week', '2 Weeks', '3 Weeks', '1 Month', '2 Months', '3 Months', '4 Months', '5 Months']"
                                        v-model="brand.holdRecordType" v-bind:placeholder="$t('Select Hold Type')">
                                    </multiselect>
                                </div>
                                <div class="form-group col-sm-12">
                                    <label></label>
                                    <div class="checkbox form-check-inline mx-2">
                                        <input type="checkbox" id="inlineCheckbox1" v-model="brand.isActive">
                                        <label for="inlineCheckbox1"> Is Active </label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer ">
                            <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveData()">Save</button>
                            <button type="button" class="btn btn-danger btn-sm" v-on:click="close()">Close</button>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <loading v-model:active="loading" :can-cancel="true" :is-full-page="false"></loading>

    </modal>
    <!-- <acessdenied v-else :model=true></acessdenied> -->
</template>
<script>
    import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

import Multiselect from "vue-multiselect";

export default {
    components: {
        Loading,
        Multiselect,
    },
    props: ['show', 'newbrand'],
    data: function () {
        return {
            brand: this.newbrand,
            loading: false,
        }
    },
    validations() {

    },
    methods: {
        close: function () {
            this.$emit('close');
        },
        SaveData: function () {
            this.loading = true;
                var root = this;
                this.$swal({
                    title: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Are you sure you want to delete?' : 'هل أنت متأكد أنك تريد حذف؟',
                    text: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'You will be able to recover this!' : 'سوف تكون قادرا على استعادة هذا!',
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: (this.$i18n.locale == 'en' || this.isLeftToRight()) ? 'Yes, delete it!' : 'نعم ، احذفها!',
                    closeOnConfirm: false,
                    closeOnCancel: false
                }).then(function (result) {
                    if (result) {
                    
                        
                    var token = '';
                    if (token == '') {
                        token = localStorage.getItem('token');
                    }
                    root.$https.post('/Region/SaveHoldTypeSetup', root.brand, {
                            headers: {
                                "Authorization": `Bearer ${token}`
                            }
                        })
                        .then(function (response) {
                                 
                                if (response.data.isAddUpdate != "") {
                                   
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Save!' : 'حفظ',
                                        text: response.data.isAddUpdate,
                                        type: 'success',
                                        confirmButtonClass: "btn btn-success",
                                        buttonsStyling: false
                                    });

                                    root.close();
                                } else {
                                    root.$swal({
                                        title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                        text: response.data.isAddUpdate,
                                        type: 'error',
                                        confirmButtonClass: "btn btn-danger",
                                        buttonsStyling: false
                                    });
                                }
                                root.loading = false
                                 
                            },
                            function () {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Data Save UnSuccessfully' : 'حفظ البيانات غير ناجح',
                                    type: 'error',
                                    confirmButtonClass: "btn btn-danger",
                                    buttonsStyling: false
                                });
                        });
                    }
                     else {
                    this.$swal('Cancelled', 'Your Hold Setup Still Save', 'info');
                     
                }
            });
        },
    },
    mounted: function () {

    }
}
</script>
