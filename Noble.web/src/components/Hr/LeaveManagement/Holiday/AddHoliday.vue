<template>
    <modal show="show">
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">Update Holiday</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>Add Holiday</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group col-sm-12">
                        <label>Name :<span class="text-danger"> *</span></label>
                        <input v-model="v$.newLeaveholiday.name.$model" type="text" class="form-control">
                    </div>
                    <div class="form-group col-sm-12">
                        <label>Date :<span class="text-danger"> *</span></label>
                        <datepicker v-model="v$.newLeaveholiday.date.$model" />
                    </div>
                    <div class="form-group col-sm-12">
                        <label>Status  :<span class="text-danger"> *</span></label>
                        <select v-model="v$.newLeaveholiday.status.$model" class="form-select orm-control select2">
                            <option disabled selected>Select Status</option>
                            <option value="1">Full Day</option>
                            <option value="2">Half Day</option>
                        </select>
                    </div>
                    <div class="form-group col-sm-12">
                        <label>Country :<span class="text-danger"> *</span></label>
                        <input v-model="v$.newLeaveholiday.country.$model" type="text" class="form-control">
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveLeavePeriod" v-bind:disabled="v$.newLeaveholiday.$invalid" v-if="type!='Edit'">{{ $t('AddColors.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveLeavePeriod" v-bind:disabled="v$.newLeaveholiday.$invalid" v-if="type=='Edit'">{{ $t('AddColors.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddColors.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
  import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';

    import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'leaveholiday', 'type'],
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
                english: '',
                render: 0,
                loading: false,
                newLeaveholiday: this.leaveholiday,
            }
        },
        validations() {
            return {
                newLeaveholiday: {
                    name: {
                        required,
                        maxLength: maxLength(30)
                    },
                    date: {
                        required,
                        maxLength: maxLength(30)
                    },
                    status: {
                        required,
                        maxLength: maxLength(30)
                    },
                    country: {
                        required,
                        maxLength: maxLength(30)
                    },
                }
            }
        },
        methods: {
            close: function () {
                this.$emit('close');
            },

            SaveLeavePeriod: function () {
                var root = this;
                this.loading = true;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                this.newLeaveholiday.status = parseInt(this.newLeaveholiday.status);
                this.$https.post('/Hr/SaveLeaveHoliday', this.newLeaveholiday, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                        if (response.data.isSuccess == true) {
                            if (root.newLeaveholiday.id == "00000000-0000-0000-0000-000000000000") {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved!' : '!تم الحفظ',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Saved Successfully!' : '!حفظ بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });

                                root.close();
                            }
                            else {

                                root.$swal({
                                    title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update!' : 'تم التحديث!',
                                    text: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Update Successfully!' : 'تم التحديث بنجاح',
                                    type: 'success',
                                    icon: 'success',
                                    showConfirmButton: false,
                                    timer: 1500,
                                    timerProgressBar: true,
                                });
                                root.close();

                            }
                        }
                        else {
                            root.$swal({
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Error!' : 'خطأ',
                                text: response.data,
                                type: 'error',
                                icon: 'error',
                                showConfirmButton: false,
                                timer: 1500,
                                timerProgressBar: true,
                            });
                        }
                    })
                    .catch(error => {
                        console.log(error)
                        root.$swal.fire(
                            {
                                icon: 'error',
                                title: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? 'Something Went Wrong!' : 'هل هناك خطب ما!',
                                text: error.response.data,

                                showConfirmButton: false,
                                timer: 5000,
                                timerProgressBar: true,
                            });

                        root.loading = false
                    })
                    .finally(() => root.loading = false);
            }
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');

        }
    }
</script>
