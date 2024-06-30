<template>
    <modal show="show" >
        <div class="modal-content">
            <div class="modal-header">
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-if="type=='Edit'">Update Leave Groups</h6>
                <h6 class="modal-title m-0" id="exampleModalDefaultLabel" v-else>Add Leave Groups</h6>
                <button type="button" class="btn-close" v-on:click="close()"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div  class="form-group col-sm-12">
                        <label>Name :<span class="text-danger"> *</span></label>
                        <input v-model="v$.leavegroup.name.$model" type="text" class="form-control">           
                    </div>
                    <div class="form-group col-sm-12">
                        <label>Employee :<span class="text-danger"> *</span></label>
                        <multiselect v-model="processContractors" :options="options" :multiple="true" track-by="name" :clear-on-select="false" :show-labels="false" label="name" v-bind:placeholder="$t('SelectOption')"
                                                 @update:modelValue="contractorListId(processContractors)">

                                    </multiselect>
                
                    </div>
                    <div  class="form-group col-sm-12">
                        <label>Details :<span class="text-danger"> *</span></label>
                        <textarea v-model="leavegroup.details" type="text" rows="3" class="form-control"></textarea>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveLeavePeriod" v-bind:disabled="v$.leavegroup.$invalid"  v-if="type!='Edit'">{{ $t('AddColors.btnSave') }}</button>
                <button type="button" class="btn btn-soft-primary btn-sm" v-on:click="SaveLeavePeriod" v-bind:disabled="v$.leavegroup.$invalid"  v-if="type=='Edit'">{{ $t('AddColors.btnUpdate') }}</button>
                <button type="button" class="btn btn-soft-secondary btn-sm" v-on:click="close()">{{ $t('AddColors.btnClear') }}</button>
            </div>
            <loading v-model:active="loading" :can-cancel="false" :is-full-page="true"></loading>
        </div>



    </modal>
</template>


<script>
    import clickMixin from '@/Mixins/clickMixin'
    
    import Multiselect from 'vue-multiselect';
      import Loading from 'vue-loading-overlay';
    import 'vue-loading-overlay/dist/css/index.css';
   import { required, maxLength } from '@vuelidate/validators'; import useVuelidate from '@vuelidate/core'
    export default {
        props: ['show', 'newleavegroup', 'type'],
        mixins: [clickMixin],
        components: {
            Loading,
            Multiselect
        },
          setup() {
            return { v$: useVuelidate() }
        },

        data: function () {
            return {
                leavegroup: this.newleavegroup,
                arabic: '',
                english: '',
                render: 0,
                loading: false,
                showEmployee:false,
                employeeId:[],
                listEmployee:[],
                options: [],
                contractorId: [],
                processContractors: [],
                
            }
        },
        validations() {
           return{
             leavegroup: {
                name: {
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
                

                this.$https.post('/Hr/SaveLeaveGroup', this.leavegroup, { headers: { "Authorization": `Bearer ${token}` } })
                    .then(function (response) {
                    if (response.data.isSuccess == true) {
                        if (root.leavegroup.id == "00000000-0000-0000-0000-000000000000") {

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
            },
            contractorListId: function (value) {
                var root = this;
                root.leavegroup.leaveGroupEmployees = [];
                value.forEach(function (val) {
                    root.leavegroup.leaveGroupEmployees.push({
                        employeeId: val.id
                    })
                });
            },
            getEmployeeData: function (x, value) {
                
                var root = this;
                var token = '';
                if (token == '') {
                    token = localStorage.getItem('token');
                }
                
                this.$https.get('/EmployeeRegistration/EmployeeList?IsDropDown=' + true , { headers: { "Authorization": `Bearer ${token}` } }).then(function (response) {
                    
                    if (response.data != null) {
                        response.data.results.forEach(function (result) {
                            root.options.push({
                                id: result.id,
                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.englishName != '' && result.englishName != null) ? result.code + ' ' + result.englishName : result.code + ' ' + result.arabicName : (result.arabicName != '' && result.arabicName != null) ? result.code + ' ' + result.arabicName : result.code + ' ' + result.englishName
                            })
                            if (x) {
                                if (value != undefined) {
                                    value.forEach(function (ids) {

                                        if (ids.employeeId == result.id) {

                                            root.processContractors.push({
                                                id: result.id,
                                                name: (root.$i18n.locale == 'en' || root.isLeftToRight()) ? (result.englishName != '' && result.englishName != null) ? result.code + ' ' + result.englishName : result.code + ' ' + result.arabicName : (result.arabicName != '' && result.arabicName != null) ? result.code + ' ' + result.arabicName : result.code + ' ' + result.englishName
                                            });
                                            // root.contractorListId(root.processContractors);
                                        }
                                    })

                                }
                            }

                        })
                    }
                });
            },
        },
        mounted: function () {
            this.english = localStorage.getItem('English');
            this.arabic = localStorage.getItem('Arabic');
            
            this.listEmployee = this.leavegroup.leaveGroupEmployees;
            this.leavegroup.leaveGroupEmployees.forEach(item => {
                        this.contractorId.push(item.employeeId);
            });
            if(this.type == 'Add')
            {
                this.getEmployeeData(false, this.leavegroup.leaveGroupEmployees);
            }
            if(this.type == 'Edit')
            {
                this.showEmployee = true;
                this.getEmployeeData(true, this.leavegroup.leaveGroupEmployees);
            }
        }
    }
</script>
